# DnDGen.RollGen Constitution

> **Read the [DnDGen constitution](https://github.com/DnDGen/.github/blob/main/constitution/CONSTITUTION.md)
> first, in full, every time this file is read.** Fetch it from
> https://raw.githubusercontent.com/DnDGen/.github/main/constitution/CONSTITUTION.md rather than
> relying on memory or an earlier copy. Its principles, constraints, workflow, and governance apply
> to this repository, and every plan, task list, analysis, and review is checked against it as well
> as against this file. This file adds only what is specific to this repository, under the DnDGen
> constitution's labels. If the DnDGen constitution can't be read, stop and say so: don't continue
> from this file alone.
<!-- Keep the pointer above exactly as written: it is what makes every Spec Kit command read the
DnDGen constitution. See https://github.com/DnDGen/.github/tree/main/constitution -->

## Core Principles

### I. Consistency Is King

**I.2 (RollGen)**

- *Example:* `AlbatrossExpressionEvaluator` still converts boolean results through their string,
  so a boolean expression requested as a number stays invalid. Converting them directly would read
  more simply, but it changes which expressions are valid, so it is not a style fix.

### II. Tests Are the Spec

**II.2.b.iii (RollGen)**

- *Example:* `Limits.Quantity` and `Limits.Die` are 10,000, so validity is tested at `10000d10000`,
  `10001d10000`, and `10000d10001`, and the same way for keeping and transforming.

**II.2.c (RollGen)** Coverage is the line and branch coverage of `DnDGen.RollGen.dll`, combined
across the unit, IoC, integration, and stress test projects, excluding generated code (`*.g.cs`,
from `[GeneratedRegex]`). It is at 100%, so it stays there.

**II.3.c (RollGen)**

- *Example:* A test of a random roll asserts the whole range of results the roll can produce, and
  repeats, rather than expecting a particular result. Unit tests that need a particular result
  give the roll a mocked `Random` instead.

### III. Leave It Better Than You Found It

No additions.

## Technology & Release Constraints

- **Stack**: C# on the latest .NET LTS, currently `net10.0`. Albatross.Expression evaluates
  expressions, and Ninject wires up `Dice`, both as dependencies of the package. NUnit, with Moq
  for test doubles and DnDGen.Stress for the stress tests.
- **Versioning and release**: A NuGet package, versioned and released as the DnDGen constitution
  describes. MAJOR also covers making a valid expression invalid, changing the range of results a
  roll can produce, and raising the minimum .NET, Ninject, or Albatross.Expression major version.
  On `main`, `cicd/build.yml` builds and runs the unit, IoC, integration, and stress tests (the
  stress tests in the `Stress` configuration) with code coverage, then tags the commit with
  `<Version>`; `cicd/release.yml` publishes to NuGet.org and creates a GitHub release.
- **README as contract**: The README documents every public member of `Dice`, `PartialRoll`,
  `RollHelper`, `Limits`, `Utils`, `DiceFactory`, and `RollGenModuleLoader`, including every
  overload, constant, and optional parameter with its default, and the expression syntax that
  `Roll` and `IsValid` accept. Its code samples are checked by the integration `ReadmeTests`. It
  states which .NET version each major version requires. The theme song is in `docs/theme-song`.

## Governance

This repository follows the DnDGen constitution's Governance. The additions in this file are
amended by pull request to this repository and versioned the same way.

**Version**: 1.0.0 | **Ratified**: 2026-10-30 | **Last Amended**: 2026-10-30
