#pragma once

#include <vector>

#include "../cipher.hpp"

namespace Cipher { namespace Text {
	cipher_t to_letter(char character);
	
	cipher_t to_letternumber(char character);
	
	template<class T> std::vector<cipher_t> to_ciphertext(const T& str) {
		std::vector<cipher_t> contents = std::vector<cipher_t>(str.size());
		
		for(const char c : str) {
			cipher_t encoded = to_letter(c);
			if(encoded != 255) contents.push_back(encoded);
		}
		
		contents.shrink_to_fit();
		
		return contents;
	}
} }
