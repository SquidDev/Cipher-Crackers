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

## Usage
Whilst this is MIT licensed, and so you're free to use this I do request that you try to avoid using this in competitions. It's far more satisfying putting the tools together yourself. If you are interested in writing your own crackers, I recommend the [Practical Cryptography](http://practicalcryptography.com/cryptanalysis/) website, a resource that proved invaluable when writing this. This project is intended as an educational resource, not just for easy wins.
