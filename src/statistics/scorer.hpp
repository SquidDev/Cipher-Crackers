#pragma once

#include <array>
#include <math.h>

#include "../cipher.hpp"
#include "quadgrams.hpp"

namespace Cipher { namespace Statistics {
	template<int N, int Pow, class T> inline double score(const T& text, const std::array<double, Pow> scores) {
		double score = 0;

		size_t size = text.size();
		if(size < N) return 0;

		// Have to build up 'n' previous
		cipher_t previous[N - 1];
		size_t i = 0;
		for (; i < N - 1; i++) {
			previous[i] = text[i] * static_cast<cipher_t>(pow(26, N - 1 - i));
		}

		for(; i < size; i++) {
			// The previous array is composed of [i * 26 * 26, (i + 1) * 26, i + 2]
			cipher_t thisCharacter = text[i];

			unsigned int sum = thisCharacter;
			for (size_t x = 0; x < N - 2; x++) {
				sum += previous[x];
				previous[x] = previous[x + 1] * 26;
			}

			sum += previous[N - 2];

			score += scores[sum];
			previous[N - 2] = thisCharacter * 26;
		}

		return score;
	}
	
	template<class T> double score_quadgrams(const T& text) {
		return score<4, 456976, T>(text, quadgrams);
	}
}}
