/*
 *
 *	The Solitaire encryption algorithm programmed in C.
 *	See <http://www.counterpane.com/solitaire.html> for details.
 *
 *	Version: 20001004
 *	Copyright(C) 2000 Harald Arnesen, <gurre@start.no>
 *
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation; either version 2 of the License, or
 *(at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
 *
 */

#include <ctype.h>
#include <stdarg.h>
#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <unistd.h>

#include "solitaire.h"

/* The deck is used everywhere, so it is global. */
static int deck[DECKSIZE];

/*
 * Just a simple test driver.
 * Need to add decrypt mode, error checks,...
 */
int main(int argc, char *argv[]) {
	int inchar, numchars;
	make_deck();
	if(argc > 1) {
		init_deck(argv[1]);
	}
	numchars = 0;
	while((inchar = get_alpha()) != EOF) {
		put_alpha(inchar + keystream());
	}
	putchar('\n');
	return EXIT_SUCCESS;
}

/*
 * For normal use:
 * 	Convert bottom card to number 1-53.
 * 	Cut that number of cards from top of deck.
 * For keying the deck:
 * 	Cut(pos) cards from top of deck.
 * Place these cards just above bottom card.
 */
void count_cut(int pos) {
	int card, tmp[DECKSIZE];
	if(pos == BOTTOMCARD) {
		card = deck[pos];
	} else {
		card = pos;
	}
	if(isjoker(card)) {
		return;
	}
	int i = TOPCARD, j = ++card;
	while(j < BOTTOMCARD) {
		tmp[i++] = deck[j++];
	}
	j = TOPCARD;
	while(j < card) {
		tmp[i++] = deck[j++];
	}
	for(i = TOPCARD; i < BOTTOMCARD; i++) {
		deck[i] = tmp[i];
	}
}

/*
 * All manipulation of the deck.
 */
void do_deck() {
	move_joker(JOKER_A);
	move_joker(JOKER_B);
	move_joker(JOKER_B);
	triple_cut();
	count_cut(BOTTOMCARD);
}

/*
 * Read a letter, convert it to a number from 1 to 26.
 */
int get_alpha() {
	int c;
	do {
		if((c = getchar()) == EOF) {
			return c;
		}
	} while(!isalpha(c));
	return(toupper(c) - 64);
}

/*
 * Use a passphrase to order the deck.
 * Skip non-alpha characters in passphrase.
 */
void init_deck(char *key) {
	while(*key) {
		if(isalpha(*key)) {
			do_deck();
			count_cut((toupper(*key)) - 64);
		}
		*key += 1;
	}
}

/*
 * Is the card one of the jokers?
 */
int isjoker(int card) {
	return((card == JOKER_A) ||(card == JOKER_B));
}

/*
 * Find the output card.
 * If the card is a joker, find a new card.
 */
int keystream() {
	int card;
	do {
		do_deck();
		card = deck[TOPCARD];
		if(card == JOKER_B) {
			card--;
		}
		card = deck[card + 1];
	} while(isjoker(card));
	return(card > 26 ? card - 26 : card);
}

/*
 * Make an unkeyed deck.
 */
void make_deck() {
	for(int i = TOPCARD; i <= BOTTOMCARD; i++) {
		deck[i] = i;
	}
}

inline static int find_joker(int start) {
	for(; start <= BOTTOMCARD; start++) {
		if(isjoker(deck[start])) return start;
	}

	puts("Issue with finding joker");
	return -1;
}

/*
 * Move a joker one card down.
 * If it is the bottom card, move it below the top card.
 * The function is called twice to move the B joker.
 */
void move_joker(int joker) {
	int pos_joker = find_joker(TOPCARD);
	if(pos_joker == BOTTOMCARD) {
		rotate_deck(joker);
		pos_joker = 1;
	}
	deck[pos_joker] = deck[pos_joker + 1];
	deck[pos_joker + 1] = joker;
}

/*
 * Convert a number 1-52 to an uppercase letter.
 */
int put_alpha(int c) {
	return putchar(c > 26 ? c + 38 : c + 64);
}

/*
 * Move a joker from the bottom of the deck to the top.
 */
void rotate_deck(int joker) {
	int tmp[DECKSIZE];
	for(int i = TOPCARD; i < BOTTOMCARD; i++) {
		tmp[i + 1] = deck[i];
	}
	tmp[TOPCARD] = joker;
	for(int i = TOPCARD; i <= BOTTOMCARD; i++) {
		deck[i] = tmp[i];
	}
}

/*
 * Swap the cards above the first joker
 * with the cards below the second joker.
 */
void triple_cut() {
	int first_joker = find_joker(TOPCARD);
	int second_joker = find_joker(first_joker + 1);
	int tmp[DECKSIZE];

	int j = TOPCARD;
	int i = second_joker + 1;
	while(i <= BOTTOMCARD) {
		tmp[j++] = deck[i++];
	}

	i = first_joker;
	while(i <= second_joker) {
		tmp[j++] = deck[i++];
	}
	i = TOPCARD;
	while(i < first_joker) {
		tmp[j++] = deck[i++];
	}
	for(i = TOPCARD; i <= BOTTOMCARD; i++) {
		deck[i] = tmp[i];
	}
}
