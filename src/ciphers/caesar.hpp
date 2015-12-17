#pragma once

#include "../cipher.hpp"

namespace Cipher { namespace Ciphers {
	template<class Text, class Scorer> class CaesarCipher {
		private:
			const Scorer scorer;
		
		public:
			CaesarCipher(const Scorer scorer) : scorer(scorer) { }
			
			CipherResult<Text, cipher_t> crack(const Text& text) const;
			Text decode(const Text& text) const;
	}
} }
