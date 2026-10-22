using Albatross.Expression;
using Albatross.Expression.Parsing;
using DnDGen.RollGen.Expressions;
using DnDGen.RollGen.PartialRolls;
using Ninject.Modules;
using System;

namespace DnDGen.RollGen.IoC.Modules
{
    internal class CoreModule : NinjectModule
    {
        public override void Load()
        {
            Bind<Random>().ToSelf().InSingletonScope();
            Bind<Dice>().To<DomainDice>();
            Bind<PartialRollFactory>().To<DomainPartialRollFactory>();
            Bind<ExpressionEvaluator>().To<AlbatrossExpressionEvaluator>().WhenInjectedInto<ExpressionEvaluatorCachingProxy>();
            Bind<ExpressionEvaluator>().To<ExpressionEvaluatorCachingProxy>().WhenInjectedInto<ExpressionEvaluatorNumericProxy>().InSingletonScope();
            Bind<ExpressionEvaluator>().To<ExpressionEvaluatorNumericProxy>().InSingletonScope();
            Bind<IParser>().ToMethod(c => new ParserBuilder().BuildDefault());
        }
    }
}