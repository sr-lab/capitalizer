# Capitalizer
Capital letter (and symbol) heat-mapper and password capitalisation utility.

## Overview
This repo is a bit of a mix, and contains several projects:

### Capitalizer
Very very simple little utility that will capitalize the first occurrence of any letter on each line of a given input file. This can be useful if, for example, you're using a password cracking algorithm that doesn't support capital letters such as [the original implementation of PCFGs for password cracking](https://sites.google.com/site/reusablesec/Home/password-cracking-tools/probablistic_cracker) by Matt Weir et al.

```
Usage: Capitalizer <input_file>
```

Output ends up on stdout, one line of input becomes one line of output. Input is of the format:

```
password
123456
Aaaaaa
letmein
123abc
```

Anything already capitalized (or anything that can't be capitalized) won't be touched. Here's the output for above:

```
Password
123456
Aaaaaa
Letmein
123Abc
```

### Capitalizer.Mapper
A utility that will map the occurrences of capital letters against offset from the beginning of strings given in an input file (one per line).

### Capitalizer.SymbolMapper
A utility that will map the occurrences of symbols against offset from the beginning of strings given in an input file (one per line).
