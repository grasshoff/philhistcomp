# Background

The Pirahã language, spoken by the eponymous tribe of the amazonian forrest is widely considered to
be one of the most peculiar languages in the world, with a very small vocabulary and lacking words
for most things beyond experiental knowledge. The Pirahã do not have a creation myth nor care about
the past beyond the people they have either met in person or someone they have met in person has met 
in person. Remarkably they also lack word for exact quantities, number words.

## Research objects

**Research objects**: a corpus of conversations with members of the Pirahã tribe translated into english.

**Research question**: Can the Pirahã language express arithmetic propositions?

**Hypothesis**: The Pirahã language cannot express arithmetic propostitions.

**Basis for the hypothesis**: The intuition would be that for arithmetic propositions we need number words, and the Pirahã
language seems to lack number words, only having words for inexact quantities. But this is just an intuition and not actual
proof that my hypothesis is correct.

**Assumptions**:
1. Meaning is use, I will assume that the meaning of words is determined by the way they are used which. As our research
data is composed of a certain amount of conversations I will use those to determine the meanings of the words that I
need to translate. 

2. Non-contradiction principle

3. Truthfulness of the proposed translations.

**Data for the proof**
The sentence I want to analyze in Pirahã is the following very basic proposition "1=1". To translate this
proposition into Pirahã I have written a script that crawls the entire data set to find usages of the two
words "1" and "=". The closest translation to "1" that I have found is "hoi", the closest translation for "="
I have found is "ia". "ia" seems to mean "is" rather than "=", but its the only candidate that could 
come into question. In English the sentence "1 is 1" does not mean the same as "1 = 1", perhaps already because
one is used in a mathematical context and the other one in natural language. I learned in predicata logic that
the "=" relationship could be substituted by logical equivalence, in otherwords the bidirectional "->". I would
say that both in the case of "1 is 1" and "1 = 1" we could substitute each of these propositions by "1 <--> 1".
Because if "1 is 1", from "1" follows "1", and viceversa. Needless to say the same goes for the equals sign. Hence,
for the purposes at hand, I think it would be legitimate to use the translation for "is" (which has a translation in 
Pirahã) as a substitute for "=".

**Argument**:
Let us assume for the sake of the argument that the contrary of what we want to prove is the case, for that matter:
"Pirahã can express arithmetic propositions"

If Pirahã can express arithmetic propositions it can express simple arithmetic propositions like 1 = 1

The translation for 1 = 1 in Pirahã is "hoi ia hoi", hence, in some sense, "1 = 1 = hoi ia hoi".

"hoi ia hoi" can be translated back to 1 = 1. From statement "1=1" follows "1=1". "1=1" is true, so from
a true statement follows a true statement. Let us use for the truth expressed by "1=1" the variable "P". Therefore from "P" follows 
P -> P.

But "hoi ia hoi" can also be translated back to "1 = 2". "1 = 2" is false on the other hand, or in other words is not-P ("-P")

Hence, using Pirahã translations, we can derive both a true and a false statement when translating a simple arithmetic
proposition like "1 = 1". In other words, we can derive both "P" and non-"P". Therefore our assumption,
"Pirahã can express arithmetics" must be dropped, and the contrary is true. The negation of this proposition would be,
"It is not the case that Pirahã can express arithmetics" or in other words "Pirahã cannot express arithmetics".