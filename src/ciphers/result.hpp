#pragma once

namespace Cipher { namespace Ciphers {
	template<class Text, class Key> CipherResult {
		public:
			const Text text;
			const Key key;
			const double score;
	}
} }