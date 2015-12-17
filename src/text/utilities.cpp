#include "utilities.hpp"

namespace Cipher { namespace Text {
	cipher_t to_letter(char character) {
		if (character >= 'A' && character <= 'Z') {
			return static_cast<cipher_t>(character - 'A');
		} else if (character >= 'a' && character <= 'z') {
			return static_cast<cipher_t>(character - 'a');
		}

		return 255;
	}
	
	cipher_t to_letternumber(char character) {
		if (character >= 'A' && character <= 'Z') {
			return static_cast<cipher_t>(character - 'A');
		} else if (character >= 'a' && character <= 'z') {
			return static_cast<cipher_t>(character - 'a');
		} else if (character == '#') {
			return 26;
		} else if (character >= '0' && character <= '9') {
			return static_cast<cipher_t>(character - '0' + 27); // After '#' symbol
		}

		return 255;
	}
} }
