using System;
using System.IO;
using System.Runtime.InteropServices;
using JP.Base.Errors.Logging.Support;

namespace JP.Base.Errors.Logging
{
    internal class MiniDumpManager
    {
        private string dumpFilePath = string.Empty;

        public MiniDumpManager()
        {
            dumpFilePath = ConfigReader.MiniDumpPath;
        }

        public string DumpFilePath
        {
            get { return dumpFilePath; }
            set { dumpFilePath = value; }
        }

        private enum MINIDUMP_TYPE
        {
            //Include just the information necessary to capture stack traces for all existing threads in a process.
            MiniDumpNormal = 0,

            //Include the data sections from all loaded modules. This results in the inclusion of global variables, which can make the minidump file significantly larger. For per-module control, use the ModuleWriteDataSeg enumeration value from MODULE_WRITE_FLAGS.
            MiniDumpWithDataSegs = 1,

            //Include all accessible memory in the process. The raw memory data is included at the end, so that the initial structures can be mapped directly without the raw memory information. This option can result in a very large file.
            MiniDumpWithFullMemory = 2,

            //Stack and backing store memory written to the minidump file should be filtered to remove all but the pointer values necessary to reconstruct a stack trace. Typically, this removes any private information.
            MiniDumpWithHandleData = 4,

            //Include high-level information about the operating system handles that are active when the minidump is made.
            MiniDumpFilterMemory = 8,

            //Stack and backing store memory should be scanned for pointer references to modules in the module list. If a module is referenced by stack or backing store memory, the ModuleWriteFlags member of the MINIDUMP_CALLBACK_OUTPUT structure is set to ModuleReferencedByMemory.
            MiniDumpScanMemory = 10,

            //the following aren't supported under XP
            //Include information from the list of modules that were recently unloaded, if this information is maintained by the operating system.
            MiniDumpWithUnloadedModules = 20,

            //Include pages with data referenced by locals or other stack memory. This option can increase the size of the minidump file significantly.
            MiniDumpWithIndirectlyReferencedMemory = 40,

            //Filter module paths for information such as user names or important directories. This option may prevent the system from locating the image file and should be used only in special situations.
            MiniDumpFilterModulePaths = 80,

            //Include complete per-process and per-thread information from the operating system.
            MiniDumpWithProcessThreadData = 100,

            //Scan the virtual address space for other types of memory to be included.
            MiniDumpWithPrivateReadWriteMemory = 200,

            //Reduce the data that is dumped by eliminating memory regions that are not essential to meet criteria specified for the dump. This can avoid dumping memory that may contain data that is private to the user. However, it is not a guarantee that no private information will be present.
            MiniDumpWithoutOptionalData = 400,

            //Include memory region information. For more information, see MINIDUMP_MEMORY_INFO_LIST.
            MiniDumpWithFullMemoryInfo = 800,

            //Include thread state information. For more information, see MINIDUMP_THREAD_INFO_LIST.
            MiniDumpWithThreadInfo = 1000,

            //Include all code and code-related sections from loaded modules to capture executable content. For per-module control, use the ModuleWriteCodeSegs enumeration value from MODULE_WRITE_FLAGS.
            MiniDumpWithCodeSegs = 2000
        }

        public string SaveMiniDump()
        {
            string dumpPath = (!string.IsNullOrEmpty(dumpFilePath) ? dumpFilePath : Path.GetDirectoryName(Path.GetTempFileName()));
            string dumpFile = Path.ChangeExtension(Path.GetTempFileName(), ".mdmp");

            dumpFile = Path.Combine(dumpPath, Path.GetFileName(dumpFile));

            bool bolResult = false;

            using (System.Diagnostics.Process dumpProcess = System.Diagnostics.Process.GetCurrentProcess())
            {
                try
                {
                    using (FileStream objDumpFile = new FileStream(dumpFile, FileMode.Create))
                    {
                        //Call the API
                        bolResult = MiniDumpWriteDump(dumpProcess.Handle, dumpProcess.Id, objDumpFile.SafeFileHandle.DangerousGetHandle(), MINIDUMP_TYPE.MiniDumpWithDataSegs, (IntPtr)0, (IntPtr)0, (IntPtr)0);
                    }

                    dumpFile = (bolResult ? dumpFile : string.Empty);

                    dumpFilePath = dumpFile;
                }
                catch (Exception ex)
                {
                    dumpFilePath = "Could Not Save Mini Dump File: " + ex.Message;
                    dumpFile = dumpFilePath;
                }

                return dumpFile;
            }
        }

        [DllImport("dbghelp.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        private static extern bool MiniDumpWriteDump(IntPtr hProcess, Int32 ProcessId, IntPtr hFile, MINIDUMP_TYPE DumpType, IntPtr ExceptionParam, IntPtr UserStreamParam, IntPtr CallackParam);
    }
}