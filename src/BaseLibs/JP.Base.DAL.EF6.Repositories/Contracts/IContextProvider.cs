using JP.Base.DAL.EF6.Contracts;

namespace JP.Base.Implementations.DAL.EF6.Repositories.Contracts
{
    /// <summary>
    /// represents a factory contract that enables the providing of a <see cref="IDbContext"/> that represents an implementation of <seealso cref="System.Data.Entity.DbContext"/>
    /// <para>there could be several ways to implement this factory, following is an example on how you could achieve this using Ninject Dependency Injection framework:</para>
    /// <code>
    /// public class DALModule : NinjectModule
    ///  {
    ///      public override void Load()
    ///      {
    ///          var scopeName = "UnitOfWorkScope";
    ///
    ///          //auto-implement factory
    ///          //https://github.com/ninject/Ninject.Extensions.Factory/wiki/Factory-interface
    ///          Bind<IContextProvider>().ToFactory();
    ///
    ///          //we need the contextpreservingGet in order to make the named scope
    ///          //this link contains information about this:
    ///          //http://stackoverflow.com/questions/25693370/cant-figure-out-why-ninject-named-scope-isnt-working-as-expected
    ///          Bind(typeof(IDbContext)).ToMethod(ctx => ctx.ContextPreservingGet<CasiraghiContext>()).InNamedScope(scopeName);
    ///      }
    ///  }
    /// </code>
    /// </summary>
    public interface IContextProvider
    {
        /// <summary>
        /// returns an <see cref="IDbContext"/> that represents the DBContext currently used for repositories interactions
        /// </summary>
        /// <returns></returns>
        IDbContext ProvideContext();
    }
}