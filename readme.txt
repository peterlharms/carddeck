Notes:
1. shuffle() was designed to exactly mimic how a person shuffles.
2. If the requirement is just to make the deck random, there are a couple of alternatives:
  a. have shuffle() simply create a new deck which contains one of the remaining cards at random, one at a time.
  b. if the shuffle is needed only to support the deal, the deal itself could return and delete a random card, 
     instead of dealing from the top.  
     In this case shuffle() could simply set a flag "isShuffled" to true, and the deal could deal random if isShuffled, 
     and from the top if not.  This would be a very efficient alternative.
3. There are a couple of "hard-coded" items for a poker deck., such as 52 cards and the 13 poker card values.
   This could be made more general, but should be done as refactoring when a requirement for, say, a pinochle deck, 
   is presented.  Do to that now would introduce unneeded complication.
