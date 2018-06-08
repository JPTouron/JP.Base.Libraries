using System;
using JP.Base.DAL.Model;
using JP.Base.DAL.UnitOfWork;
using JP.Base.ViewModel;
using System.Collections;
using System.Collections.Generic;

namespace JP.Base.Logic.Implementations
{



    public abstract class CompositeBaseLogic<TIUnitOfWork,TResult>
            where TIUnitOfWork : IBaseUnitOfWork
    {
        protected LogicAction currentAction;
        protected IUoWFactory<TIUnitOfWork> factory;

        public CompositeBaseLogic(IUoWFactory<TIUnitOfWork> factory)
        {
            this.factory = factory;
        }

        public abstract TResult DoShit();


    }

    

    public class DecorativeLogicGetFunc : CompositeBaseLogic<IBaseUnitOfWork, IEnumerable<object>>
    {

        public DecorativeLogicGetFunc(IUoWFactory<IBaseUnitOfWork> factory) : base(factory)
        {
        }

        public override IEnumerable<object> DoShit()
        {
            using (var uow = factory.CreateUoW())
            {

                return uow.Execute(() =>
                {
                    // do some delete shit...

                    return new List<object>();
                });
            }

        }


    }


    public class DecorativeLogicDeleteAction : CompositeLogicAction<IBaseUnitOfWork>
    {
        CompositeBaseLogic<IBaseUnitOfWork> logic;

        public DecorativeLogicDeleteAction(IUoWFactory<IBaseUnitOfWork> factory) : base(factory)
        {
        }

        public void SetLogicalAction(CompositeBaseLogic<IBaseUnitOfWork> logic)
        {
            this.logic = logic;
        }


        public override void ExecuteOperation()
        {
            using (var uow = factory.CreateUoW())
            {

                uow.Execute(() =>
                               {
                                   // do some delete shit...
                               });



            }


        }
    }


    public class mycomposedlogic : CompositeBaseLogic<IBaseUnitOfWork>
    {
        public mycomposedlogic(IUoWFactory<IBaseUnitOfWork> factory) : base(factory)
        {
        }
    }

}