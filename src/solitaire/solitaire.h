
#define TOPCARD     1
#define BOTTOMCARD  54
#define DECKSIZE    BOTTOMCARD + 1
#define JOKER_A     BOTTOMCARD - 1
#define JOKER_B     BOTTOMCARD

void count_cut(int pos);
void do_deck(void);
int get_alpha(void);
void init_deck(char *key);
int isjoker(int card);
int keystream(void);
void make_deck(void);
void move_joker(int joker);
int put_alpha(int c);
void rotate_deck(int joker);
void triple_cut(void);
