# RollGen

Rolls a set of dice, as determined by the D20 die system.

[![Build Status](https://dev.azure.com/dndgen/DnDGen/_apis/build/status%2FDnDGen.RollGen%20-%20Build?branchName=main)](https://dev.azure.com/dndgen/DnDGen/_build/latest?definitionId=13&branchName=main) [![Theme Song](https://img.shields.io/badge/theme_song-Suno-ff69b4)](https://suno.com/s/RvnVPcPR4ZTGIEt7)

## Installing

The project is on [NuGet](https://www.nuget.org/packages/DnDGen.RollGen).

```
dotnet add package DnDGen.RollGen
```

Version 17 and later require .NET 10. Use version 16.x if you are still on .NET 8.

## Syntax

### Use

To use RollGen, simply use the verbose, fluid syntax:

```C#
var standardRoll = dice.Roll(4).d6().AsSum();
var customRoll = dice.Roll(92).d(66).AsSum();
var expressionRoll = dice.Roll("5+3d4*2").AsSum();

var chainedRolls = dice.Roll().d2().d(5).Keeping(1).d6().AsSum(); //1d2d5k1d6 Evaluated left to right -> ((1d2)d5k1)d6

var individualRolls = dice.Roll(4).d6().AsIndividualRolls();
var parsedRolls = dice.Roll("5+3d4*2").AsIndividualRolls(); //NOTE: This will only return 1 roll, the same as AsSum()

var keptRolls = dice.Roll(4).d6().Keeping(3).AsIndividualRolls(); //Returns the highest 3 rolls
var expressionKeptRolls = dice.Roll("3d4k2").AsSum(); //Returns the sum of 2 highest rolls

var averageRoll = dice.Roll(4).d6().AsPotentialAverage(); //Returns the average roll for the expression.  For here, it will return 14.
var expressionAverageRoll = dice.Roll("5+3d4*3").AsPotentialAverage(); //5+7.5*3, returning 27.5 

var minRoll = dice.Roll(4).d6().AsPotentialMinimum(); //Returns the minimum roll for the expression.  For here, it will return 4.
var expressionMinRoll = dice.Roll("5+3d4*3").AsPotentialMinimum(); //5+3*3, returning 14

var maxRoll = dice.Roll(4).d6().AsPotentialMaximum(); //Returns the maximum roll for the expression.  For here, it will return 24.
var expressionMaxRoll = dice.Roll("5+3d4*3").AsPotentialMaximum(); //5+12*3, returning 41 

var success = dice.Roll().Percentile().AsTrueOrFalse(); //Returns true if high (51-100), false if low (1-50)
var customPercentageSuccess = dice.Roll().Percentile().AsTrueOrFalse(.9); //Returns true if > 90, false if <= 90
var customRollSuccess = dice.Roll().Percentile().AsTrueOrFalse(90); //Returns true if >= 90, false if < 90
var expressionSuccess = dice.Roll("5+3d4*2").AsTrueOrFalse(); //Returns true if high, false if low
var explicitExpressionSuccess = dice.Roll("2d6 >= 1d12").AsTrueOrFalse(); //Evaluates boolean expression after rolling

var containsRoll = dice.ContainsRoll("This contains a roll of 4d6k3 for rolling stats"); //will return true here
var summedSentence = dice.ReplaceRollsWithSumExpression("This contains a roll of 4d6k3 for rolling stats"); //returns "This contains a roll of (5 + 3 + 2) for rolling stats"
var rolledSentence = dice.ReplaceExpressionWithTotal("This contains a roll of 4d6k3 for rolling stats"); //returns "This contains a roll of 10 for rolling stats"
var rolledComplexSentenceMin = dice.ReplaceWrappedExpressions<double>("Fireball does {min(4d6,10) + 0.5} damage"); //returns "Fireball does 7.5 damage"
var rolledComplexSentenceMax = dice.ReplaceWrappedExpressions<double>("Fireball does {max(4d6,10) + 0.5} damage"); //returns "Fireball does 15.5 damage"

var optimizedRollWithFewestDice = RollHelper.GetRollWithFewestDice(1, 9); //returns "4d3-3", because it uses only 1 kind of dice compared to "1d8+1d2-1"
var optimizedRoll = RollHelper.GetRollWithMostEvenDistribution(4, 9); //returns "1d6+3", which is the most evenly-distributed roll possible, whether optimizing for dice or distribution
var optimizedRollWithMultipleDice = RollHelper.GetRollWithMostEvenDistribution(1, 9); //returns "1d8+1d2-1", because it more evenly-distributed than "4d3-3"
var optimizedRollWithMultipliers = RollHelper.GetRollWithMostEvenDistribution(1, 36, allowMultipliers: true); //returns "(1d12-1)*3+1d3"
var optimizedRollWithNonstandardDice = RollHelper.GetRollWithMostEvenDistribution(1, 36, allowNonstandardDice: true); //returns "1d36"
var optimizedRollWithMultipliersAndNonstandardDice = RollHelper.GetRollWithMostEvenDistribution(1, 45, true, true); //returns "(1d3-1)*15+(1d3-1)*5+1d5"

var explodedRolls = dice.Roll(4).d6().Explode().AsIndividualRolls(); //If a 6 is rolled, then an additional roll is performed.  I.E., 3 + 6 + 5 + 4 + 1
var expressionExplodedRolls = dice.Roll("3d4!").AsSum(); //Return the sum of the rolls, including bonus rolls from explosion on rolls of 4
var expressionExplodedKeptRolls = dice.Roll("3d4!k2").AsSum(); //Returns the sum of 2 highest rolls, including bonus rolls from explosion on rolls of 4
var expressionExplodedMultipleRolls = dice.Roll("3d4!e3").AsSum(); //Return the sum of the rolls, including bonus rolls from explosion on rolls of 3 or 4
var expressionExplodedMultipleKeptRolls = dice.Roll("3d4e1e2k2").AsSum(); //Returns the sum of 2 highest rolls, including bonus rolls from explosion on rolls of 1 or 2

var transformedRolls = dice.Roll(3).d6().Transforming(1).AsIndividualRolls(); //If a 1 is rolled, we will count it as a 6.  I.E., 3 + 1 + 6 = 3 + 6 + 6
var transformedMultipleRolls = dice.Roll("3d6t1t5").AsSum(); //Return the sum of the rolls, including 1's and 5's transformed to 6's
var transformedExplodedKeptRolls = dice.Roll("3d6!t1k2").AsSum(); //Returns the sum of 2 highest rolls, including bonus rolls from explosion and transforming 1's to 6's.
var transformedCustomRolls = dice.Roll("3d6t1:2").AsSum(); //Return the sum of the rolls, transforming 1's into 2's.

//Returns a qualitative description of the roll, based on percentages
var description = dice.Describe("3d6", 9); //Returns "Bad"
description = dice.Describe("3d6", 13); //Returns "Good"
description = dice.Describe("3d6", 12, "Bad", "Average", "Good"); //Returns "Average"
description = dice.Describe("3d6", 6, "Bad", "Average", "Good"); //Returns "Bad"
description = dice.Describe("3d6", 16, "Bad", "Average", "Good"); //Returns "Good"

//Returns whether the roll is valid
var valid = dice.IsValid("3d6+1"); //Returns TRUE
valid = dice.IsValid("(3)d(6)+1"); //Returns TRUE
valid = dice.IsValid("(1d2)d(3d4)+5d6"); //Returns TRUE
valid = dice.IsValid("10000d10000"); //Returns TRUE
valid = dice.IsValid("(100d100)d(100d100)"); //Returns TRUE

valid = dice.IsValid("(0)d(6)+1"); //Returns FALSE, because quantity is too low
valid = dice.IsValid("(3.1)d(6)+1"); //Returns FALSE, because decimals are not allowed for dice operations
valid = dice.IsValid("0d6+1"); //Returns FALSE, because quantity is too low
valid = dice.IsValid("10001d10000"); //Returns FALSE, because quantity is too high
valid = dice.IsValid("(100d100+1)d(100d100)"); //Returns FALSE, because quantity could potentially be above the 10,000 limit

valid = dice.IsValid("(3)d(-6)+1"); //Returns FALSE, because die is too low
valid = dice.IsValid("(3)d(6.1)+1"); //Returns FALSE, because decimals are not allowed for dice operations
valid = dice.IsValid("3d0+1"); //Returns FALSE, because die is too low
valid = dice.IsValid("10000d10001"); //Returns FALSE, because die is too high
valid = dice.IsValid("(100d100)d(100d100+1)"); //Returns FALSE, because die could potentially be above the 10,000 limit

valid = dice.IsValid("4d6k3"); //Returns TRUE
valid = dice.IsValid("(4)d(6)k(3)"); //Returns TRUE
valid = dice.IsValid("(4)d(6)k(-1)"); //Returns FALSE, because keep value is too low
valid = dice.IsValid("(4)d(6)k(3.1)"); //Returns FALSE, because decimals are not allowed for dice operations
valid = dice.IsValid("4d6k10000"); //Returns TRUE
valid = dice.IsValid("4d6k10001"); //Returns FALSE, because keep value is too high
valid = dice.IsValid("4d6k(100d100+1)"); //Returns FALSE, because keep value could potentially be above the 10,000 limit

valid = dice.IsValid("2d3!"); //Returns TRUE
valid = dice.IsValid("2d3!e2"); //Returns TRUE
valid = dice.IsValid("2d3!e2e1"); //Returns FALSE, because it explodes on all values

valid = dice.IsValid("3d6t1"); //Returns TRUE
valid = dice.IsValid("3d6t1t2"); //Returns TRUE
valid = dice.IsValid("3d6t7"); //Returns TRUE
valid = dice.IsValid("3d6t0"); //Returns FALSE, because transform target is too low
valid = dice.IsValid("3d6t6:0"); //Returns TRUE
valid = dice.IsValid("3d6t10001"); //Returns FALSE, because transform target is too high
valid = dice.IsValid("3d6t6:10001"); //Returns TRUE

valid = dice.IsValid("avg(1d12, 2d6, 3d4, 4d3, 6d2)"); //Returns TRUE, because this is a valid Albatross function
valid = dice.IsValid("bad(1d12, 2d6, 3d4, 4d3, 6d2)"); //Returns FALSE, because "bad" is not a valid Albatross function

valid = dice.IsValid("this is not a roll"); //Returns FALSE
valid = dice.IsValid("this contains 3d6, but is not a roll"); //Returns FALSE
valid = dice.IsValid("9266+90210-42*600/1337%1336+96d(783d82%45+922-2022/337)-min(1d2, 3d4, 5d6)+max(1d2, 3d4, 5d6)*avg(1d2, 3d4, 5d6)"); //Returns TRUE

```

Important things to note:

1. When retrieving individual rolls from an expression, only the sum of the expression is returned.  This is because determining what an "individual roll" is within a complex expression is not certain.  As an example, what would individual rolls be for `1d2+3d4`?
2. Mathematical expressions supported are `+`, `-`, `*`, `/`, and `%`, as well as die rolls as demonstrated above.  Spaces are allowed in the expression strings.  Parenthetical expressions are also allowed.
3. For replacement methods on `Dice`, there is an option to do "lenient" replacements (optional boolean).  The difference:
    a. "1d2 ghouls and 2d4 zombies" strict -> "1 ghouls and 5 zombies"
    b. "1d2 ghouls and 2d4 zombies" lenient -> "1 ghouls an3 zombies" (it reads "and 2d4" as "an(d 2d4)")

#### Order of Operations

In regards to the operators one can apply to a roll (Keeping, Exploding, Transforming), this is the order of operations:

1. Explode
2. Transform
3. Keep

One can specify these commands in any order, as they will be evaluated in their order of operation.  For example, all of these rolls will parse the same: `4d3!t2k1`, `4d3!k1t2`, `4d3t2!k1`, `4d3t2k1!`, `4d3k1!t2`, `4d3k1t2!` - all will be evaluated as `4d3`, exploding on a `3`, then transforming `2` into `3`, then keeping the highest roll.

Beyond this, order of operations is respected as outlined by the Albatross documentation: https://rushuiguan.github.io/expression/articles/operations.html#the-precedence-of-infix-operations

The documentation also outlines supported functions (such as `min` and `max`) that can be used: https://rushuiguan.github.io/expression/articles/operations.html

### Getting `Dice` Objects

`Dice` is an interface, and its implementation is internal to RollGen. Get an instance through Ninject, using the `RollGenModuleLoader`, or through `DiceFactory` if you are not using Ninject.

```C#
using DnDGen.RollGen.IoC;
using Ninject;

var kernel = new StandardKernel();
var rollGenModuleLoader = new RollGenModuleLoader();

rollGenModuleLoader.LoadModules(kernel);
```

Your particular syntax for how the Ninject injection should work will depend on your project (class library, web site, etc.).  Each of the examples below work independently.  You should use the one that is appropriate for your project.

```C#
[Inject] //Ninject property
public Dice MyDice { get; set; }

public MyClass()
{ }

public int Roll()
{
    return MyDice.Roll(4).d6().Keeping(3).AsSum();
}
```

```C#
private readonly Dice myDice;

public MyClass(Dice dice) //This works if you are calling Ninject to build MyClass
{
    myDice = dice;
}

public int Roll()
{
    return myDice.Roll(4).d6().Keeping(3).AsSum();
}
```

```C#
public MyClass()
{ }

public int Roll()
{
    var myDice = DiceFactory.Create(); //Located in DnDGen.RollGen.IoC
    return myDice.Roll(4).d6().Keeping(3).AsSum();
}
```


## Reference

Every public type is in the `DnDGen.RollGen` namespace, except `DiceFactory` and `RollGenModuleLoader`, which are in `DnDGen.RollGen.IoC`.

### `Dice`

Starts rolls, and finds and replaces rolls in text.

| Method | What it does |
| --- | --- |
| `Roll(int quantity = 1)` | Starts a roll of `quantity` dice. Follow it with a die, such as `d6()`. |
| `Roll(string rollExpression)` | Starts a roll from an expression, such as `"4d6k3+2"`. |
| `Roll(PartialRoll roll)` | Starts a roll whose quantity is another roll. |
| `ReplaceWrappedExpressions<T>(string str, string openexpr = "{", string closeexpr = "}", char? openexprescape = '\\')` | Rolls every expression in `str` that is wrapped in `openexpr` and `closeexpr`, and replaces it with its total as a `T`, or with `True` or `False` when `T` is `bool`. An `openexpr` straight after `openexprescape` is left alone, and the escape removed. A `null` escape means nothing is escaped. |
| `ReplaceExpressionWithTotal(string expression, bool lenient = false)` | Replaces every roll in `expression` with its total, then any arithmetic left, such as `2+3`, with its value. |
| `ReplaceRollsWithSumExpression(string expression, bool lenient = false)` | Replaces every roll in `expression` with its individual rolls as a sum, such as `(5 + 3 + 2)`, or a single number for a single die. |
| `ContainsRoll(string expression, bool lenient = false)` | Whether `expression` contains a roll. |
| `Describe(string rollExpression, int roll, params string[] descriptions)` | Describes `roll` by where it falls between the lowest and highest possible results of `rollExpression`, choosing from `descriptions`, worst first. With no `descriptions`, it chooses between `Bad` and `Good`. |
| `IsValid(string rollExpression)` | Whether RollGen can roll `rollExpression`, within the `Limits`. |

With `lenient`, a roll can run into the word before it; see note 3 above.

### `PartialRoll`

A roll being built. Every method that returns a `PartialRoll` adds to the roll and returns the same roll, so calls chain. The `As` methods roll it, or work out what it could roll, and throw an `InvalidOperationException` if it isn't valid.

| Property | What it is |
| --- | --- |
| `CurrentRollExpression` | The roll expression built so far. |

| Method | What it does |
| --- | --- |
| `d(int die)`, `d(string die)`, `d(PartialRoll roll)` | Rolls the quantity so far on dice with this many sides. |
| `d2()`, `d3()`, `d4()`, `d6()`, `d8()`, `d10()`, `d12()`, `d20()` | Rolls the quantity so far on that die. |
| `Percentile()` | Rolls the quantity so far on a d100. |
| `Keeping(int amountToKeep)`, `Keeping(string amountToKeep)`, `Keeping(PartialRoll roll)` | Keeps only the highest rolls, this many of them. |
| `Explode()` | Whenever a die rolls its highest value, rolls it again, and counts both. |
| `ExplodeOn(int rollToExplode)`, `ExplodeOn(string rollToExplode)`, `ExplodeOn(PartialRoll roll)` | Explodes on this roll as well. |
| `Transforming(int rollToTransform, int? transformTarget = null)`, `Transforming(string rollToTransform, string transformTarget = null)`, `Transforming(PartialRoll roll, PartialRoll transformTarget = null)` | Counts this roll as `transformTarget` instead, or as the die's highest value when `transformTarget` is `null`. |
| `Plus(string expression)`, `Plus(double value)`, `Plus(PartialRoll roll)` | Adds an expression, a number, or another roll. |
| `Minus(string expression)`, `Minus(double value)`, `Minus(PartialRoll roll)` | Subtracts an expression, a number, or another roll. |
| `Times(string expression)`, `Times(double value)`, `Times(PartialRoll roll)` | Multiplies by an expression, a number, or another roll. |
| `DividedBy(string expression)`, `DividedBy(double value)`, `DividedBy(PartialRoll roll)` | Divides by an expression, a number, or another roll. |
| `Modulos(string expression)`, `Modulos(double value)`, `Modulos(PartialRoll roll)` | Takes the remainder after dividing by an expression, a number, or another roll. |
| `AsSum()`, `AsSum<T>()` | Rolls, and returns the total, as an `int` or a `T`. |
| `AsIndividualRolls()`, `AsIndividualRolls<T>()` | Rolls, and returns each die's roll, as `int`s or `T`s. For an expression that isn't a single roll, it returns only the total; see note 1 above. |
| `AsPotentialAverage()` | The average result. |
| `AsPotentialMinimum()`, `AsPotentialMinimum<T>()` | The lowest possible result, as an `int` or a `T`. |
| `AsPotentialMaximum(bool includeExplode = true)`, `AsPotentialMaximum<T>(bool includeExplode = true)` | The highest possible result, as an `int` or a `T`. Exploding dice could roll forever, so with `includeExplode` an exploding roll's highest result is taken as 10 times what it would be without exploding. |
| `AsTrueOrFalse(double threshold = 0.5)` | Rolls, and returns `true` if the result is above `threshold` of the way from the lowest possible result to the highest. A boolean expression, such as `2d6 >= 1d12`, is evaluated instead. |
| `AsTrueOrFalse(int threshold)` | Rolls, and returns `true` if the result is at least `threshold`. Throws an `InvalidOperationException` for a boolean expression. |
| `IsValid()` | Whether RollGen can roll this, within the `Limits`. |

### `RollHelper`

Builds roll expressions that produce every whole number in a range, and nothing outside it. A range of one number gives that number. When `lower` is above `upper`, `GetRollWithFewestDice`, and `GetRollWithMostEvenDistribution` with its default options, throw an `ArgumentException`.

| Method | What it does |
| --- | --- |
| `GetRollWithFewestDice(int lower, int upper)` | The roll with the fewest dice, such as `4d3-3` for 1 to 9. |
| `GetRollWithFewestDice(int baseQuantity, int lower, int upper)` | The same, for the range with `baseQuantity` taken off both ends. |
| `GetRollWithMostEvenDistribution(int lower, int upper, bool allowMultipliers = false, bool allowNonstandardDice = false)` | The roll whose results are most evenly spread, such as `1d8+1d2-1` for 1 to 9. `allowMultipliers` lets it multiply dice, such as `(1d12-1)*3+1d3`, and `allowNonstandardDice` lets it use any die, such as `1d36`. |
| `GetRollWithMostEvenDistribution(int baseQuantity, int lower, int upper, bool allowMultipliers = false, bool allowNonstandardDice = false)` | The same, for the range with `baseQuantity` taken off both ends. |

### `Limits`

| Property | What it is |
| --- | --- |
| `Quantity` | 10,000, the most dice in one roll, and the most dice it can keep. |
| `Die` | 10,000, the most sides on one die, and the highest roll it can transform. |

### `DiceFactory`

| Method | What it does |
| --- | --- |
| `Create()` | Returns a `Dice`, for when you aren't using Ninject. RollGen loads itself into a kernel of its own the first time. |

### `RollGenModuleLoader`

| Constructor | What it does |
| --- | --- |
| `RollGenModuleLoader()` | Creates a module loader. |

| Method | What it does |
| --- | --- |
| `LoadModules(IKernel kernel)` | Loads RollGen into your Ninject `kernel`, so it can build `Dice`. Loading it again does nothing. |

## Theme Song

DnDGen.RollGen has its own theme song: [Grollen -DnDGen.RollGen Theme-](https://suno.com/s/RvnVPcPR4ZTGIEt7), a tavern gambling and drinking song.
It follows this README, so a change here may need the song updated too.
See the song's [lyrics, notes, and update procedure](https://github.com/DnDGen/DnDGen.RollGen/blob/main/docs/theme-song/README.md).
