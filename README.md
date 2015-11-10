# Cipher-Crackers
[![Build status](https://ci.appveyor.com/api/projects/status/xml5w9y00myb22p0/branch/master)](https://ci.appveyor.com/project/SquidDev/cipher-crackers/branch/master)

This is a collection of utilities for solving the National Cipher Challenge.

## Ciphers Implemented
 - *Caesar shift*: Attempts every possible key and scores using quadgram statistics
 - *Substitution*: Uses a hill-climbing algorithm.
 - *Vigenere*: Guesses the key length from repeated sub-strings, then solves individual shifts using monogram statistics.
 - *Columnar Transposition*: Tries every key, scores using quadgrams
 - *Rail-fence*: Tries every key, scores using quadgrams
 - *Hill Cipher*: Finds the modular matrix inverse using cribs. Requires several cribs as not every crib is usable (the determinant of the matrix must be co-prime with 26).

## Analysis Tools
 - *Auto-spacer*: Attempts to add spaces between words
 - *N-Gram statistics*: Occurrences of words of a specific length.
 - *Cipher guesser*: Uses some of the methods described [here](http://practicalcryptography.com/cryptanalysis/text-characterisation/identifying-unknown-ciphers/) to try to identify a cipher.


## CoFH "Don't Be a Jerk" License
#### Okay, so here's the deal.

You'll notice that this repository does not have a license! By default, that means "All Rights Reserved."

That is indeed the case. All rights reserved, as far as the code is concerned.

©2015 SquidDev

#### Notice

Contribution to this repository means that you are granting me rights over the code that you choose to contribute. If you do not agree with that, do not contribute.

So, why is this here? Well, the rights are reserved, but what that really means is that I choose what to do with the rights. So here you go.

#### You CAN
- Fork and modify the code.
- Submit Pull Requests to this repository.
- Copy portions of this code for use in other projects.
- Write your own code that uses this code as a dependency.

#### You CANNOT
- Redistribute this in its entirety as source or compiled code.
- Create or distribute code which contains 50% or more Functionally Equivalent Statements* from this repository.

#### You MUST
- Maintain a visible repository of your code which is inspired by, derived from, or copied from this code. Basically, if you use it, pay it forward. You keep rights to your OWN code, but you still must make your source visible.
- Not be a jerk**. Seriously, if you're a jerk, you can't use this code. That's part of the agreement.

#### You MUST NOT
- Use this project either in source or binary form, Functionally Equivalent Statements*, derived or inspired source to compete in competitions of any form, such as the [National Cipher Challenge](http://www.cipher.maths.soton.ac.uk/) without written permission from the author.

#### Notes, License & Copyright

*A Functionally Equivalent Statement is a code fragment which, regardless of whitespace and object names, achieves the same result. Basically you can't copy the code, rename the variables, add whitespace and say it's different. It's not.

**A jerk is anyone who attempts to or intends to claim partial or total ownership of the original or repackaged code and/or attempts to or intends to redistribute original or repackaged code without prior express written permission from the owner (SquidDev).

Essentially, take this and learn from it! If you see something I can improve, tell me. Submit a Pull Request. The one catch: don't steal! A lot of effort has gone into this, and if you were to take this and call it your own, you'd basically be a big jerk.

Don't be a jerk.

--- 

Non legalese: Think for yourself, don't just use someone else's code and thoughts to win! If you are interested in writing your own crackers, I recommend the [Practical Cryptography](http://practicalcryptography.com/cryptanalysis/) website, a resource that proved invaluable when writing this. This project is indented as an educational resource, not just for easy wins.
