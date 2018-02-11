using System.ComponentModel.DataAnnotations;

namespace JP.Base.DAL.EF6.Model
{


    /// <summary>
    /// this class represents a base clas from which to build a model for EF6
    /// <para>contains an Id property and a Version property, this one will work as a timestamp preventing concurrency collisions</para>
    /// <para>more info at: https://msdn.microsoft.com/en-us/library/jj591583(v=vs.113).aspx#Anchor_7</para>
    /// </summary>
    /// <typeparam name="T">the type of the Id, usually an int</typeparam>
    public class BaseModel<T>
    {
        /// <summary>
        /// the Id for the model
        /// </summary>
        public T Id { get; set; }

        /// <summary>
        /// the concurrency collision control property
        /// </summary>
        [Timestamp]
        public byte[] Version { get; set; }
    }
}