#include <typeinfo>
#include <iostream>
#include <string>
#include <vector>

#include "statistics/scorer.hpp"
#include "text/utilities.hpp"
#include "ciphers/caesar.hpp"

int main() {
	std::string name;
	std::cin >> name;
	
	auto x = Cipher::Text::to_ciphertext(name);
	auto y = Cipher::Statistics::score_quadgrams(x);
	printf("Score is %f\n", y);
	
	new Cipher::Ciphers::CaesarCipher<typeid(x)>(x).decode(x);
}
