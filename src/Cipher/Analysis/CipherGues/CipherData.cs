using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cipher.Analysis.CipherGuess
{
    public static class CipherData
    {
        public static CipherType[] Ciphers = {
            new CipherType
            {
	            Name = "Plaintext",
	            Average = new StatsType
	            {
		            IC = 63,
		            MIC = 73,
		            MKA = 95,
		            DIC = 72,
		            EDI = 73,
		            LR = 22,
		            ROD = 50,
		            LDI = 756,
		            SDD = 303,
	            },
	            Standard = new StatsType
	            {
		            IC = 5,
		            MIC = 11,
		            MKA = 19,
		            DIC = 18,
		            EDI = 24,
		            LR = 5,
		            ROD = 6,
		            LDI = 13,
		            SDD = 23,
	            },
            },
            new CipherType
            {
	            Name = "Randomdigit",
	            Average = new StatsType
	            {
		            IC = 100,
		            MIC = 108,
		            MKA = 132,
		            DIC = 100,
		            EDI = 98,
		            LR = 21,
		            ROD = 50,
		            LDI = 0,
		            SDD = 0,
	            },
	            Standard = new StatsType
	            {
		            IC = 2,
		            MIC = 8,
		            MKA = 16,
		            DIC = 8,
		            EDI = 15,
		            LR = 3,
		            ROD = 3,
		            LDI = 0,
		            SDD = 0,
	            },
            },
            new CipherType
            {
	            Name = "Randomtext",
	            Average = new StatsType
	            {
		            IC = 38,
		            MIC = 44,
		            MKA = 60,
		            DIC = 14,
		            EDI = 14,
		            LR = 5,
		            ROD = 50,
		            LDI = 428,
		            SDD = 109,
	            },
	            Standard = new StatsType
	            {
		            IC = 1,
		            MIC = 5,
		            MKA = 12,
		            DIC = 2,
		            EDI = 5,
		            LR = 3,
		            ROD = 10,
		            LDI = 23,
		            SDD = 14,
	            },
            },
            new CipherType
            {
	            Name = "6x6Bifid",
	            Average = new StatsType
	            {
		            IC = 35,
		            MIC = 47,
		            MKA = 62,
		            DIC = 14,
		            EDI = 14,
		            LR = 4,
		            ROD = 49,
		            LDI = 298,
		            SDD = 71,
	            },
	            Standard = new StatsType
	            {
		            IC = 4,
		            MIC = 9,
		            MKA = 16,
		            DIC = 5,
		            EDI = 8,
		            LR = 3,
		            ROD = 12,
		            LDI = 53,
		            SDD = 16,
	            },
            },
            new CipherType
            {
	            Name = "6x6Playfair",
	            Average = new StatsType
	            {
		            IC = 42,
		            MIC = 51,
		            MKA = 67,
		            DIC = 32,
		            EDI = 72,
		            LR = 11,
		            ROD = 25,
		            LDI = 243,
		            SDD = 63,
	            },
	            Standard = new StatsType
	            {
		            IC = 4,
		            MIC = 9,
		            MKA = 15,
		            DIC = 9,
		            EDI = 24,
		            LR = 5,
		            ROD = 9,
		            LDI = 57,
		            SDD = 19,
	            },
            },
            new CipherType
            {
	            Name = "Amsco",
	            Average = new StatsType
	            {
		            IC = 63,
		            MIC = 72,
		            MKA = 94,
		            DIC = 44,
		            EDI = 43,
		            LR = 11,
		            ROD = 50,
		            LDI = 688,
		            SDD = 188,
	            },
	            Standard = new StatsType
	            {
		            IC = 5,
		            MIC = 10,
		            MKA = 19,
		            DIC = 10,
		            EDI = 13,
		            LR = 4,
		            ROD = 8,
		            LDI = 15,
		            SDD = 17,
	            },
            },
            new CipherType
            {
	            Name = "Bazeries",
	            Average = new StatsType
	            {
		            IC = 64,
		            MIC = 74,
		            MKA = 94,
		            DIC = 60,
		            EDI = 61,
		            LR = 17,
		            ROD = 49,
		            LDI = 477,
		            SDD = 112,
	            },
	            Standard = new StatsType
	            {
		            IC = 4,
		            MIC = 13,
		            MKA = 20,
		            DIC = 15,
		            EDI = 20,
		            LR = 5,
		            ROD = 5,
		            LDI = 44,
		            SDD = 21,
	            },
            },
            new CipherType
            {
	            Name = "Beaufort",
	            Average = new StatsType
	            {
		            IC = 42,
		            MIC = 67,
		            MKA = 78,
		            DIC = 23,
		            EDI = 23,
		            LR = 9,
		            ROD = 50,
		            LDI = 443,
		            SDD = 113,
	            },
	            Standard = new StatsType
	            {
		            IC = 3,
		            MIC = 9,
		            MKA = 17,
		            DIC = 5,
		            EDI = 9,
		            LR = 4,
		            ROD = 10,
		            LDI = 32,
		            SDD = 15,
	            },
            },
            new CipherType
            {
	            Name = "Bifid6",
	            Average = new StatsType
	            {
		            IC = 47,
		            MIC = 58,
		            MKA = 75,
		            DIC = 24,
		            EDI = 24,
		            LR = 7,
		            ROD = 48,
		            LDI = 510,
		            SDD = 119,
	            },
	            Standard = new StatsType
	            {
		            IC = 4,
		            MIC = 10,
		            MKA = 15,
		            DIC = 6,
		            EDI = 8,
		            LR = 4,
		            ROD = 10,
		            LDI = 36,
		            SDD = 16,
	            },
            },
            new CipherType
            {
	            Name = "Bifid7",
	            Average = new StatsType
	            {
		            IC = 47,
		            MIC = 58,
		            MKA = 77,
		            DIC = 24,
		            EDI = 23,
		            LR = 7,
		            ROD = 49,
		            LDI = 517,
		            SDD = 118,
	            },
	            Standard = new StatsType
	            {
		            IC = 4,
		            MIC = 9,
		            MKA = 17,
		            DIC = 6,
		            EDI = 8,
		            LR = 4,
		            ROD = 9,
		            LDI = 37,
		            SDD = 17,
	            },
            },
            new CipherType
            {
	            Name = "Cadenus",
	            Average = new StatsType
	            {
		            IC = 63,
		            MIC = 74,
		            MKA = 95,
		            DIC = 40,
		            EDI = 41,
		            LR = 10,
		            ROD = 49,
		            LDI = 657,
		            SDD = 134,
	            },
	            Standard = new StatsType
	            {
		            IC = 5,
		            MIC = 11,
		            MKA = 17,
		            DIC = 9,
		            EDI = 13,
		            LR = 4,
		            ROD = 9,
		            LDI = 17,
		            SDD = 18,
	            },
            },
            new CipherType
            {
	            Name = "Cmbifid",
	            Average = new StatsType
	            {
		            IC = 47,
		            MIC = 57,
		            MKA = 75,
		            DIC = 23,
		            EDI = 23,
		            LR = 6,
		            ROD = 50,
		            LDI = 493,
		            SDD = 114,
	            },
	            Standard = new StatsType
	            {
		            IC = 4,
		            MIC = 9,
		            MKA = 15,
		            DIC = 5,
		            EDI = 9,
		            LR = 4,
		            ROD = 10,
		            LDI = 31,
		            SDD = 16,
	            },
            },
            new CipherType
            {
	            Name = "Columnar",
	            Average = new StatsType
	            {
		            IC = 63,
		            MIC = 73,
		            MKA = 96,
		            DIC = 41,
		            EDI = 41,
		            LR = 11,
		            ROD = 50,
		            LDI = 653,
		            SDD = 128,
	            },
	            Standard = new StatsType
	            {
		            IC = 5,
		            MIC = 11,
		            MKA = 18,
		            DIC = 8,
		            EDI = 12,
		            LR = 4,
		            ROD = 7,
		            LDI = 16,
		            SDD = 15,
	            },
            },
            new CipherType
            {
	            Name = "Digrafid",
	            Average = new StatsType
	            {
		            IC = 41,
		            MIC = 53,
		            MKA = 67,
		            DIC = 17,
		            EDI = 20,
		            LR = 5,
		            ROD = 43,
		            LDI = 469,
		            SDD = 112,
	            },
	            Standard = new StatsType
	            {
		            IC = 3,
		            MIC = 7,
		            MKA = 13,
		            DIC = 4,
		            EDI = 7,
		            LR = 3,
		            ROD = 11,
		            LDI = 33,
		            SDD = 15,
	            },
            },
            new CipherType
            {
	            Name = "DoubleCheckerBoard",
	            Average = new StatsType
	            {
		            IC = 90,
		            MIC = 133,
		            MKA = 149,
		            DIC = 110,
		            EDI = 207,
		            LR = 25,
		            ROD = 13,
		            LDI = 609,
		            SDD = 133,
	            },
	            Standard = new StatsType
	            {
		            IC = 13,
		            MIC = 18,
		            MKA = 23,
		            DIC = 30,
		            EDI = 58,
		            LR = 5,
		            ROD = 7,
		            LDI = 44,
		            SDD = 19,
	            },
            },
            new CipherType
            {
	            Name = "Four_square",
	            Average = new StatsType
	            {
		            IC = 48,
		            MIC = 58,
		            MKA = 76,
		            DIC = 36,
		            EDI = 72,
		            LR = 11,
		            ROD = 28,
		            LDI = 507,
		            SDD = 114,
	            },
	            Standard = new StatsType
	            {
		            IC = 3,
		            MIC = 9,
		            MKA = 15,
		            DIC = 8,
		            EDI = 24,
		            LR = 4,
		            ROD = 8,
		            LDI = 33,
		            SDD = 16,
	            },
            },
            new CipherType
            {
	            Name = "FracMorse",
	            Average = new StatsType
	            {
		            IC = 47,
		            MIC = 53,
		            MKA = 70,
		            DIC = 42,
		            EDI = 43,
		            LR = 16,
		            ROD = 50,
		            LDI = 444,
		            SDD = 107,
	            },
	            Standard = new StatsType
	            {
		            IC = 2,
		            MIC = 8,
		            MKA = 15,
		            DIC = 9,
		            EDI = 13,
		            LR = 3,
		            ROD = 7,
		            LDI = 32,
		            SDD = 17,
	            },
            },
            new CipherType
            {
	            Name = "Grandpre",
	            Average = new StatsType
	            {
		            IC = 128,
		            MIC = 136,
		            MKA = 158,
		            DIC = 179,
		            EDI = 227,
		            LR = 33,
		            ROD = 43,
		            LDI = 0,
		            SDD = 0,
	            },
	            Standard = new StatsType
	            {
		            IC = 3,
		            MIC = 7,
		            MKA = 15,
		            DIC = 15,
		            EDI = 39,
		            LR = 3,
		            ROD = 3,
		            LDI = 0,
		            SDD = 0,
	            },
            },
            new CipherType
            {
	            Name = "Grille",
	            Average = new StatsType
	            {
		            IC = 63,
		            MIC = 74,
		            MKA = 91,
		            DIC = 42,
		            EDI = 43,
		            LR = 10,
		            ROD = 49,
		            LDI = 679,
		            SDD = 173,
	            },
	            Standard = new StatsType
	            {
		            IC = 5,
		            MIC = 12,
		            MKA = 16,
		            DIC = 9,
		            EDI = 14,
		            LR = 4,
		            ROD = 7,
		            LDI = 16,
		            SDD = 17,
	            },
            },
            new CipherType
            {
	            Name = "Gromark",
	            Average = new StatsType
	            {
		            IC = 39,
		            MIC = 46,
		            MKA = 63,
		            DIC = 15,
		            EDI = 15,
		            LR = 4,
		            ROD = 50,
		            LDI = 431,
		            SDD = 109,
	            },
	            Standard = new StatsType
	            {
		            IC = 1,
		            MIC = 7,
		            MKA = 13,
		            DIC = 3,
		            EDI = 6,
		            LR = 3,
		            ROD = 12,
		            LDI = 26,
		            SDD = 15,
	            },
            },
            new CipherType
            {
	            Name = "Gronsfeld",
	            Average = new StatsType
	            {
		            IC = 40,
		            MIC = 66,
		            MKA = 76,
		            DIC = 21,
		            EDI = 25,
		            LR = 9,
		            ROD = 42,
		            LDI = 444,
		            SDD = 111,
	            },
	            Standard = new StatsType
	            {
		            IC = 2,
		            MIC = 8,
		            MKA = 19,
		            DIC = 5,
		            EDI = 11,
		            LR = 4,
		            ROD = 14,
		            LDI = 27,
		            SDD = 15,
	            },
            },
            new CipherType
            {
	            Name = "Homophonic",
	            Average = new StatsType
	            {
		            IC = 101,
		            MIC = 108,
		            MKA = 127,
		            DIC = 116,
		            EDI = 160,
		            LR = 24,
		            ROD = 42,
		            LDI = 0,
		            SDD = 0,
	            },
	            Standard = new StatsType
	            {
		            IC = 1,
		            MIC = 6,
		            MKA = 13,
		            DIC = 7,
		            EDI = 15,
		            LR = 2,
		            ROD = 2,
		            LDI = 0,
		            SDD = 0,
	            },
            },
            new CipherType
            {
	            Name = "MonomeDinome",
	            Average = new StatsType
	            {
		            IC = 124,
		            MIC = 134,
		            MKA = 169,
		            DIC = 249,
		            EDI = 252,
		            LR = 45,
		            ROD = 49,
		            LDI = 0,
		            SDD = 0,
	            },
	            Standard = new StatsType
	            {
		            IC = 7,
		            MIC = 11,
		            MKA = 19,
		            DIC = 36,
		            EDI = 43,
		            LR = 5,
		            ROD = 2,
		            LDI = 0,
		            SDD = 0,
	            },
            },
            new CipherType
            {
	            Name = "Morbit",
	            Average = new StatsType
	            {
		            IC = 122,
		            MIC = 129,
		            MKA = 156,
		            DIC = 193,
		            EDI = 194,
		            LR = 38,
		            ROD = 49,
		            LDI = 0,
		            SDD = 0,
	            },
	            Standard = new StatsType
	            {
		            IC = 4,
		            MIC = 7,
		            MKA = 16,
		            DIC = 15,
		            EDI = 25,
		            LR = 2,
		            ROD = 2,
		            LDI = 0,
		            SDD = 0,
	            },
            },
            new CipherType
            {
	            Name = "Myszkowski",
	            Average = new StatsType
	            {
		            IC = 63,
		            MIC = 72,
		            MKA = 95,
		            DIC = 41,
		            EDI = 41,
		            LR = 11,
		            ROD = 49,
		            LDI = 657,
		            SDD = 135,
	            },
	            Standard = new StatsType
	            {
		            IC = 5,
		            MIC = 10,
		            MKA = 18,
		            DIC = 8,
		            EDI = 13,
		            LR = 4,
		            ROD = 7,
		            LDI = 18,
		            SDD = 18,
	            },
            },
            new CipherType
            {
	            Name = "Nicodemus",
	            Average = new StatsType
	            {
		            IC = 42,
		            MIC = 50,
		            MKA = 73,
		            DIC = 18,
		            EDI = 18,
		            LR = 5,
		            ROD = 50,
		            LDI = 442,
		            SDD = 112,
	            },
	            Standard = new StatsType
	            {
		            IC = 3,
		            MIC = 7,
		            MKA = 14,
		            DIC = 4,
		            EDI = 7,
		            LR = 3,
		            ROD = 10,
		            LDI = 35,
		            SDD = 15,
	            },
            },
            new CipherType
            {
	            Name = "Nihilistsub",
	            Average = new StatsType
	            {
		            IC = 144,
		            MIC = 201,
		            MKA = 195,
		            DIC = 218,
		            EDI = 266,
		            LR = 38,
		            ROD = 40,
		            LDI = 0,
		            SDD = 0,
	            },
	            Standard = new StatsType
	            {
		            IC = 11,
		            MIC = 23,
		            MKA = 30,
		            DIC = 33,
		            EDI = 42,
		            LR = 4,
		            ROD = 6,
		            LDI = 0,
		            SDD = 0,
	            },
            },
            new CipherType
            {
	            Name = "NihilistTransp",
	            Average = new StatsType
	            {
		            IC = 63,
		            MIC = 73,
		            MKA = 97,
		            DIC = 41,
		            EDI = 40,
		            LR = 10,
		            ROD = 50,
		            LDI = 654,
		            SDD = 129,
	            },
	            Standard = new StatsType
	            {
		            IC = 5,
		            MIC = 12,
		            MKA = 18,
		            DIC = 9,
		            EDI = 13,
		            LR = 4,
		            ROD = 9,
		            LDI = 17,
		            SDD = 17,
	            },
            },
            new CipherType
            {
	            Name = "Patristocrat",
	            Average = new StatsType
	            {
		            IC = 63,
		            MIC = 73,
		            MKA = 95,
		            DIC = 72,
		            EDI = 73,
		            LR = 22,
		            ROD = 50,
		            LDI = 414,
		            SDD = 106,
	            },
	            Standard = new StatsType
	            {
		            IC = 5,
		            MIC = 11,
		            MKA = 19,
		            DIC = 18,
		            EDI = 24,
		            LR = 5,
		            ROD = 6,
		            LDI = 57,
		            SDD = 23,
	            },
            },
            new CipherType
            {
	            Name = "Phillips",
	            Average = new StatsType
	            {
		            IC = 49,
		            MIC = 58,
		            MKA = 74,
		            DIC = 32,
		            EDI = 32,
		            LR = 11,
		            ROD = 49,
		            LDI = 424,
		            SDD = 106,
	            },
	            Standard = new StatsType
	            {
		            IC = 3,
		            MIC = 8,
		            MKA = 16,
		            DIC = 7,
		            EDI = 10,
		            LR = 4,
		            ROD = 9,
		            LDI = 37,
		            SDD = 17,
	            },
            },
            new CipherType
            {
	            Name = "Periodic gromark",
	            Average = new StatsType
	            {
		            IC = 38,
		            MIC = 45,
		            MKA = 63,
		            DIC = 14,
		            EDI = 15,
		            LR = 4,
		            ROD = 48,
		            LDI = 428,
		            SDD = 108,
	            },
	            Standard = new StatsType
	            {
		            IC = 1,
		            MIC = 7,
		            MKA = 14,
		            DIC = 3,
		            EDI = 6,
		            LR = 3,
		            ROD = 11,
		            LDI = 26,
		            SDD = 16,
	            },
            },
            new CipherType
            {
	            Name = "Playfair",
	            Average = new StatsType
	            {
		            IC = 50,
		            MIC = 60,
		            MKA = 79,
		            DIC = 38,
		            EDI = 72,
		            LR = 12,
		            ROD = 32,
		            LDI = 491,
		            SDD = 118,
	            },
	            Standard = new StatsType
	            {
		            IC = 4,
		            MIC = 9,
		            MKA = 18,
		            DIC = 9,
		            EDI = 24,
		            LR = 4,
		            ROD = 8,
		            LDI = 42,
		            SDD = 19,
	            },
            },
            new CipherType
            {
	            Name = "Pollux",
	            Average = new StatsType
	            {
		            IC = 100,
		            MIC = 103,
		            MKA = 121,
		            DIC = 105,
		            EDI = 105,
		            LR = 23,
		            ROD = 50,
		            LDI = 0,
		            SDD = 0,
	            },
	            Standard = new StatsType
	            {
		            IC = 0,
		            MIC = 2,
		            MKA = 9,
		            DIC = 2,
		            EDI = 4,
		            LR = 1,
		            ROD = 1,
		            LDI = 0,
		            SDD = 0,
	            },
            },
            new CipherType
            {
	            Name = "Porta",
	            Average = new StatsType
	            {
		            IC = 41,
		            MIC = 66,
		            MKA = 74,
		            DIC = 22,
		            EDI = 25,
		            LR = 9,
		            ROD = 42,
		            LDI = 432,
		            SDD = 111,
	            },
	            Standard = new StatsType
	            {
		            IC = 2,
		            MIC = 9,
		            MKA = 16,
		            DIC = 6,
		            EDI = 11,
		            LR = 4,
		            ROD = 13,
		            LDI = 35,
		            SDD = 16,
	            },
            },
            new CipherType
            {
	            Name = "Portax",
	            Average = new StatsType
	            {
		            IC = 42,
		            MIC = 51,
		            MKA = 66,
		            DIC = 18,
		            EDI = 19,
		            LR = 6,
		            ROD = 48,
		            LDI = 442,
		            SDD = 113,
	            },
	            Standard = new StatsType
	            {
		            IC = 2,
		            MIC = 7,
		            MKA = 14,
		            DIC = 3,
		            EDI = 8,
		            LR = 3,
		            ROD = 12,
		            LDI = 24,
		            SDD = 13,
	            },
            },
            new CipherType
            {
	            Name = "Progressivekey",
	            Average = new StatsType
	            {
		            IC = 38,
		            MIC = 45,
		            MKA = 63,
		            DIC = 14,
		            EDI = 13,
		            LR = 4,
		            ROD = 49,
		            LDI = 428,
		            SDD = 109,
	            },
	            Standard = new StatsType
	            {
		            IC = 1,
		            MIC = 6,
		            MKA = 13,
		            DIC = 3,
		            EDI = 5,
		            LR = 3,
		            ROD = 14,
		            LDI = 24,
		            SDD = 15,
	            },
            },
            new CipherType
            {
	            Name = "Progkey beaufort",
	            Average = new StatsType
	            {
		            IC = 38,
		            MIC = 45,
		            MKA = 63,
		            DIC = 14,
		            EDI = 14,
		            LR = 4,
		            ROD = 49,
		            LDI = 429,
		            SDD = 109,
	            },
	            Standard = new StatsType
	            {
		            IC = 1,
		            MIC = 6,
		            MKA = 14,
		            DIC = 3,
		            EDI = 6,
		            LR = 3,
		            ROD = 12,
		            LDI = 26,
		            SDD = 14,
	            },
            },
            new CipherType
            {
	            Name = "Quagmire2",
	            Average = new StatsType
	            {
		            IC = 41,
		            MIC = 65,
		            MKA = 75,
		            DIC = 21,
		            EDI = 25,
		            LR = 8,
		            ROD = 42,
		            LDI = 431,
		            SDD = 109,
	            },
	            Standard = new StatsType
	            {
		            IC = 2,
		            MIC = 8,
		            MKA = 15,
		            DIC = 5,
		            EDI = 10,
		            LR = 4,
		            ROD = 14,
		            LDI = 32,
		            SDD = 16,
	            },
            },
            new CipherType
            {
	            Name = "Quagmire3",
	            Average = new StatsType
	            {
		            IC = 42,
		            MIC = 66,
		            MKA = 76,
		            DIC = 22,
		            EDI = 24,
		            LR = 8,
		            ROD = 43,
		            LDI = 444,
		            SDD = 110,
	            },
	            Standard = new StatsType
	            {
		            IC = 2,
		            MIC = 9,
		            MKA = 18,
		            DIC = 5,
		            EDI = 10,
		            LR = 4,
		            ROD = 12,
		            LDI = 36,
		            SDD = 17,
	            },
            },
            new CipherType
            {
	            Name = "Quagmire4",
	            Average = new StatsType
	            {
		            IC = 41,
		            MIC = 65,
		            MKA = 75,
		            DIC = 21,
		            EDI = 23,
		            LR = 8,
		            ROD = 44,
		            LDI = 440,
		            SDD = 111,
	            },
	            Standard = new StatsType
	            {
		            IC = 2,
		            MIC = 8,
		            MKA = 18,
		            DIC = 5,
		            EDI = 10,
		            LR = 4,
		            ROD = 13,
		            LDI = 33,
		            SDD = 17,
	            },
            },
            new CipherType
            {
	            Name = "Ragbaby",
	            Average = new StatsType
	            {
		            IC = 41,
		            MIC = 49,
		            MKA = 71,
		            DIC = 18,
		            EDI = 18,
		            LR = 6,
		            ROD = 49,
		            LDI = 473,
		            SDD = 112,
	            },
	            Standard = new StatsType
	            {
		            IC = 1,
		            MIC = 8,
		            MKA = 14,
		            DIC = 4,
		            EDI = 6,
		            LR = 4,
		            ROD = 11,
		            LDI = 23,
		            SDD = 15,
	            },
            },
            new CipherType
            {
	            Name = "Redefence",
	            Average = new StatsType
	            {
		            IC = 63,
		            MIC = 72,
		            MKA = 94,
		            DIC = 41,
		            EDI = 43,
		            LR = 10,
		            ROD = 49,
		            LDI = 653,
		            SDD = 128,
	            },
	            Standard = new StatsType
	            {
		            IC = 5,
		            MIC = 10,
		            MKA = 16,
		            DIC = 10,
		            EDI = 16,
		            LR = 4,
		            ROD = 7,
		            LDI = 18,
		            SDD = 15,
	            },
            },
            new CipherType
            {
	            Name = "RunningKey",
	            Average = new StatsType
	            {
		            IC = 39,
		            MIC = 56,
		            MKA = 74,
		            DIC = 16,
		            EDI = 16,
		            LR = 4,
		            ROD = 49,
		            LDI = 445,
		            SDD = 107,
	            },
	            Standard = new StatsType
	            {
		            IC = 4,
		            MIC = 18,
		            MKA = 22,
		            DIC = 8,
		            EDI = 15,
		            LR = 5,
		            ROD = 19,
		            LDI = 35,
		            SDD = 23,
	            },
            },
            new CipherType
            {
	            Name = "Seriatedpfair",
	            Average = new StatsType
	            {
		            IC = 48,
		            MIC = 56,
		            MKA = 75,
		            DIC = 25,
		            EDI = 25,
		            LR = 7,
		            ROD = 49,
		            LDI = 484,
		            SDD = 115,
	            },
	            Standard = new StatsType
	            {
		            IC = 3,
		            MIC = 9,
		            MKA = 19,
		            DIC = 6,
		            EDI = 9,
		            LR = 4,
		            ROD = 8,
		            LDI = 38,
		            SDD = 17,
	            },
            },
            new CipherType
            {
	            Name = "Swagman",
	            Average = new StatsType
	            {
		            IC = 62,
		            MIC = 72,
		            MKA = 90,
		            DIC = 39,
		            EDI = 39,
		            LR = 10,
		            ROD = 50,
		            LDI = 650,
		            SDD = 135,
	            },
	            Standard = new StatsType
	            {
		            IC = 5,
		            MIC = 11,
		            MKA = 17,
		            DIC = 7,
		            EDI = 12,
		            LR = 4,
		            ROD = 6,
		            LDI = 18,
		            SDD = 16,
	            },
            },
            new CipherType
            {
	            Name = "Tridigital",
	            Average = new StatsType
	            {
		            IC = 122,
		            MIC = 134,
		            MKA = 161,
		            DIC = 195,
		            EDI = 197,
		            LR = 38,
		            ROD = 49,
		            LDI = 0,
		            SDD = 0,
	            },
	            Standard = new StatsType
	            {
		            IC = 8,
		            MIC = 15,
		            MKA = 22,
		            DIC = 29,
		            EDI = 37,
		            LR = 4,
		            ROD = 3,
		            LDI = 0,
		            SDD = 0,
	            },
            },
            new CipherType
            {
	            Name = "Trifid",
	            Average = new StatsType
	            {
		            IC = 42,
		            MIC = 53,
		            MKA = 68,
		            DIC = 18,
		            EDI = 18,
		            LR = 6,
		            ROD = 51,
		            LDI = 462,
		            SDD = 112,
	            },
	            Standard = new StatsType
	            {
		            IC = 3,
		            MIC = 8,
		            MKA = 14,
		            DIC = 5,
		            EDI = 8,
		            LR = 3,
		            ROD = 12,
		            LDI = 37,
		            SDD = 15,
	            },
            },
            new CipherType
            {
	            Name = "Trisquare",
	            Average = new StatsType
	            {
		            IC = 43,
		            MIC = 51,
		            MKA = 64,
		            DIC = 21,
		            EDI = 21,
		            LR = 7,
		            ROD = 49,
		            LDI = 503,
		            SDD = 119,
	            },
	            Standard = new StatsType
	            {
		            IC = 2,
		            MIC = 5,
		            MKA = 11,
		            DIC = 3,
		            EDI = 6,
		            LR = 2,
		            ROD = 6,
		            LDI = 23,
		            SDD = 14,
	            },
            },
            new CipherType
            {
	            Name = "Trisquare HR",
	            Average = new StatsType
	            {
		            IC = 43,
		            MIC = 52,
		            MKA = 65,
		            DIC = 21,
		            EDI = 21,
		            LR = 7,
		            ROD = 50,
		            LDI = 512,
		            SDD = 120,
	            },
	            Standard = new StatsType
	            {
		            IC = 1,
		            MIC = 5,
		            MKA = 11,
		            DIC = 3,
		            EDI = 5,
		            LR = 3,
		            ROD = 7,
		            LDI = 23,
		            SDD = 13,
	            },
            },
            new CipherType
            {
	            Name = "Two square",
	            Average = new StatsType
	            {
		            IC = 49,
		            MIC = 60,
		            MKA = 77,
		            DIC = 36,
		            EDI = 72,
		            LR = 11,
		            ROD = 28,
		            LDI = 542,
		            SDD = 121,
	            },
	            Standard = new StatsType
	            {
		            IC = 3,
		            MIC = 8,
		            MKA = 16,
		            DIC = 9,
		            EDI = 24,
		            LR = 4,
		            ROD = 8,
		            LDI = 33,
		            SDD = 18,
	            },
            },
            new CipherType
            {
	            Name = "Twosquarespiral",
	            Average = new StatsType
	            {
		            IC = 47,
		            MIC = 59,
		            MKA = 76,
		            DIC = 34,
		            EDI = 72,
		            LR = 11,
		            ROD = 25,
		            LDI = 501,
		            SDD = 119,
	            },
	            Standard = new StatsType
	            {
		            IC = 3,
		            MIC = 8,
		            MKA = 15,
		            DIC = 7,
		            EDI = 24,
		            LR = 4,
		            ROD = 9,
		            LDI = 36,
		            SDD = 17,
	            },
            },
            new CipherType
            {
	            Name = "Vigautokey",
	            Average = new StatsType
	            {
		            IC = 39,
		            MIC = 45,
		            MKA = 62,
		            DIC = 15,
		            EDI = 14,
		            LR = 4,
		            ROD = 50,
		            LDI = 434,
		            SDD = 111,
	            },
	            Standard = new StatsType
	            {
		            IC = 1,
		            MIC = 6,
		            MKA = 12,
		            DIC = 3,
		            EDI = 5,
		            LR = 3,
		            ROD = 12,
		            LDI = 23,
		            SDD = 16,
	            },
            },
            new CipherType
            {
	            Name = "Vigenere",
	            Average = new StatsType
	            {
		            IC = 42,
		            MIC = 65,
		            MKA = 74,
		            DIC = 22,
		            EDI = 26,
		            LR = 8,
		            ROD = 42,
		            LDI = 438,
		            SDD = 106,
	            },
	            Standard = new StatsType
	            {
		            IC = 2,
		            MIC = 8,
		            MKA = 15,
		            DIC = 6,
		            EDI = 11,
		            LR = 4,
		            ROD = 13,
		            LDI = 33,
		            SDD = 16,
	            },
            },
            new CipherType
            {
	            Name = "period 7 Vigenere",
	            Average = new StatsType
	            {
		            IC = 42,
		            MIC = 67,
		            MKA = 78,
		            DIC = 23,
		            EDI = 23,
		            LR = 9,
		            ROD = 50,
		            LDI = 437,
		            SDD = 108,
	            },
	            Standard = new StatsType
	            {
		            IC = 3,
		            MIC = 9,
		            MKA = 17,
		            DIC = 5,
		            EDI = 8,
		            LR = 4,
		            ROD = 10,
		            LDI = 34,
		            SDD = 17,
	            },
            },
            new CipherType
            {
	            Name = "Vigslidefair",
	            Average = new StatsType
	            {
		            IC = 40,
		            MIC = 63,
		            MKA = 72,
		            DIC = 18,
		            EDI = 25,
		            LR = 6,
		            ROD = 40,
		            LDI = 436,
		            SDD = 112,
	            },
	            Standard = new StatsType
	            {
		            IC = 2,
		            MIC = 9,
		            MKA = 16,
		            DIC = 4,
		            EDI = 9,
		            LR = 3,
		            ROD = 11,
		            LDI = 34,
		            SDD = 15,
	            },
            },
            new CipherType
            {
	            Name = "Route Transp",
	            Average = new StatsType
	            {
		            IC = 63,
		            MIC = 73,
		            MKA = 92,
		            DIC = 46,
		            EDI = 47,
		            LR = 12,
		            ROD = 50,
		            LDI = 675,
		            SDD = 162,
	            },
	            Standard = new StatsType
	            {
		            IC = 5,
		            MIC = 11,
		            MKA = 17,
		            DIC = 14,
		            EDI = 18,
		            LR = 6,
		            ROD = 7,
		            LDI = 33,
		            SDD = 50,
	            },
            },
        };

        public static byte[,] LogDi = 
        {
            {4,7,8,7,4,6,7,5,7,3,6,8,7,9,3,7,3,9,8,9,6,7,6,5,7,4},
            {7,4,2,0,8,1,1,1,6,3,0,7,2,1,7,1,0,6,5,3,7,1,2,0,6,0},
            {8,2,5,2,7,3,2,8,7,2,7,6,2,1,8,2,2,6,4,7,6,1,3,0,4,0},
            {7,6,5,6,8,6,5,5,8,4,3,6,6,5,7,5,3,6,7,7,6,5,6,0,6,2},
            {9,7,8,8,8,7,6,6,7,4,5,8,7,9,7,7,5,9,9,8,5,7,7,6,7,3},
            {7,4,5,3,7,6,4,4,7,2,2,6,5,3,8,4,0,7,5,7,6,2,4,0,5,0},
            {7,5,5,4,7,5,5,7,7,3,2,6,5,5,7,5,2,7,6,6,6,3,5,0,5,1},
            {8,5,4,4,9,4,3,4,8,3,1,5,5,4,8,4,2,6,5,7,6,2,5,0,5,0},
            {7,5,8,7,7,7,7,4,4,2,5,8,7,9,7,6,4,7,8,8,4,7,3,5,0,5},
            {5,0,0,0,4,0,0,0,3,0,0,0,0,0,5,0,0,0,0,0,6,0,0,0,0,0},
            {5,4,3,2,7,4,2,4,6,2,2,4,3,6,5,3,1,3,6,5,3,0,4,0,5,0},
            {8,5,5,7,8,5,4,4,8,2,5,8,5,4,8,5,2,4,6,6,6,5,5,0,7,1},
            {8,6,4,3,8,4,2,4,7,1,0,4,6,4,7,6,1,3,6,5,6,1,4,0,6,0},
            {8,6,7,8,8,6,9,6,8,4,6,6,5,6,8,5,3,5,8,9,6,5,6,3,6,2},
            {6,6,7,7,6,8,6,6,6,3,6,7,8,9,7,7,3,9,7,8,9,6,8,4,5,3},
            {7,3,3,3,7,3,2,6,7,2,1,7,3,2,7,6,0,7,6,6,6,0,3,0,4,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,6,0,0,0,0,0},
            {8,6,6,7,9,6,6,5,8,3,6,6,6,6,8,6,3,6,8,8,6,5,6,0,7,1},
            {8,6,7,6,8,6,5,7,8,4,6,6,6,6,8,7,4,5,8,9,7,4,7,0,6,2},
            {8,6,6,5,8,6,5,9,8,3,3,6,6,5,9,6,2,7,8,8,7,4,7,0,7,2},
            {6,6,7,6,6,4,6,4,6,2,3,7,7,8,5,6,0,8,8,8,3,3,4,3,4,3},
            {6,1,0,0,8,0,0,0,7,0,0,0,0,0,5,0,0,0,1,0,2,1,0,0,3,0},
            {7,3,3,4,7,3,2,8,7,2,2,4,4,6,7,3,0,5,5,5,2,1,4,0,3,1},
            {4,1,4,2,4,2,0,3,5,1,0,1,1,0,3,5,0,1,2,5,2,0,2,2,3,0},
            {6,6,6,6,6,6,5,5,6,3,3,5,6,5,8,6,3,5,7,6,4,3,6,2,4,2},
            {4,0,0,0,5,0,0,0,3,0,0,2,0,0,3,0,0,0,1,0,2,0,0,0,4,4},
        };

        public static byte[,] Sdd = 
        {
            {0,3,4,2,0,0,1,0,0,0,4,5,2,6,0,2,0,4,4,3,0,6,0,0,3,5},
            {0,0,0,0,6,0,0,0,0,9,0,7,0,0,0,0,0,0,0,0,7,0,0,0,7,0},
            {3,0,0,0,2,0,0,6,0,0,8,0,0,0,6,0,5,0,0,0,3,0,0,0,0,0},
            {1,6,0,0,1,0,0,0,4,4,0,0,0,0,0,0,0,0,0,1,0,0,4,0,1,0},
            {0,0,4,5,0,0,0,0,0,3,0,0,3,2,0,3,6,5,4,0,0,4,3,8,0,0},
            {3,0,0,0,0,5,0,0,2,1,0,0,0,0,5,0,0,2,0,4,1,0,0,0,0,0},
            {2,0,0,0,1,0,0,6,1,0,0,0,0,0,2,0,0,1,0,0,2,0,0,0,0,0},
            {5,0,0,0,7,0,0,0,5,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {0,0,5,0,0,0,4,0,0,0,1,1,3,7,0,0,0,0,5,3,0,5,0,0,0,8},
            {0,0,0,0,6,0,0,0,0,0,0,0,0,0,5,0,0,0,0,0,9,0,0,0,0,0},
            {0,0,0,0,6,0,0,0,5,0,0,0,0,4,0,0,0,0,0,0,0,0,1,0,0,0},
            {2,0,0,4,2,0,0,0,3,0,0,7,0,0,0,0,0,0,0,0,0,0,0,0,7,0},
            {5,5,0,0,5,0,0,0,2,0,0,0,0,0,2,6,0,0,0,0,2,0,0,0,6,0},
            {0,0,4,7,0,0,8,0,0,2,2,0,0,0,0,0,3,0,0,4,0,0,0,0,0,0},
            {0,2,0,0,0,8,0,0,0,0,4,0,5,5,0,2,0,4,0,0,7,4,5,0,0,0},
            {3,0,0,0,3,0,0,0,0,0,0,5,0,0,5,7,0,6,0,0,3,0,0,0,0,0},
            {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,9,0,0,0,0,0},
            {1,0,0,0,4,0,0,0,2,0,4,0,0,0,2,0,0,0,0,0,0,0,0,0,5,0},
            {1,1,0,0,0,0,0,1,2,0,0,0,0,0,1,4,4,0,1,4,2,0,4,0,0,0},
            {0,0,0,0,0,0,0,8,3,0,0,0,0,0,3,0,0,0,0,0,0,0,2,0,0,0},
            {0,4,3,0,0,0,5,0,0,0,0,6,2,3,0,6,0,6,5,3,0,0,0,0,0,6},
            {0,0,0,0,8,0,0,0,4,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
            {6,0,0,0,2,0,0,6,6,0,0,0,0,0,2,0,0,0,0,0,0,0,0,0,0,0},
            {3,0,7,0,1,0,0,0,2,0,0,0,0,0,0,9,0,0,0,5,0,0,0,6,0,0},
            {1,6,2,0,0,2,0,0,0,6,0,0,2,0,6,2,1,0,2,1,0,0,6,0,0,0},
            {2,0,0,0,8,0,0,0,0,6,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,9},
        };
    }
}
