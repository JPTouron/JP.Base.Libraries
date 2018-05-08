namespace JP.Base.DAL.EF6.UnitOfWork
{
    /// <summary>
    /// this interface represents a factory for a UnitOfWork
    /// </summary>
    public interface IUoWFactoryEf
    {
        /// <summary>
        /// <para>this method returns a <see cref="IBaseUnitOfWorkEf"/></para>
        /// <para>
        /// in case you use ninject for this:
        /// do not use Get... as the method name or there will be trouble:
        /// https://github.com/ninject/ninject.extensions.factory/wiki/Factory-interface%3a-Referencing-Named-Bindings
        /// http://stackoverflow.com/questions/10479384/activationexception-when-using-tofactory-in-ninject
        /// </para>
        /// </summary>
        IBaseUnitOfWorkEf CreateUoW();
    }
}