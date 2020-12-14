from spacy.matcher import Matcher

def disagreement_pairs(list_of_affirmative_sentences, list_of_negated_sentences):
    """Returns a list of sentence pairs of which the sentences express disagreement
    parameter: list_of_affirmative_sentences: List of sentences without a negation expression
    parameter: list_of_negated_sentences: List of sentences that contain a negation expression
    returns: List of sentence pairs
    """
    span_matcher = Matcher(nlp.vocab, validate=True)

    span_pattern_1 = [{"POS": "VERB", "DEP": "ROOT", "LEMMA": {"IN": ['reveal', 'show', 'suggest', 'support']}}]
    span_pattern_2 = [{"POS": "VERB", "DEP": "xcomp", "LEMMA": "confirm"}]

    span_matcher.add("SPAN_ID", None, span_pattern_1, span_pattern_2)
    
    # List of pairs of disagreeing sentences: pairs_of_disagreeing_sentences
    # This list shall be returned be the function
    pairs_of_disagreeing_sentences = []
    
    # 1. Loop ("outer loop"): Iterate over all Doc-objects in 'list_of_affirmative_sentences'
    for doc in list_of_affirmative_sentences:
        # Slice Doc-object into Span-object with the help of 'span_matcher': span1
        for match_id, start, end in span_matcher(doc):
            span1 = doc[end:]
         
        # 2. Loop ("inner loop"): Iterate over all Doc-objects in 'list_of_negated_sentences'
        for doc_neg in list_of_negated_sentences:
            # Slice Doc-object into Span-object with the help of 'span_matcher': span2 
            for match_id, start, end in span_matcher(doc_neg):
                span2 = doc_neg[end + 1:]
            
            # If Span-object 1 and Span-object 2 have a certain degree of similarity
            # then make a pair of the corresponding sentences and add the pair to 
            # the list 'pairs_of_disagreeing_sentences'
            if span1.similarity(span2) >= 0.83:
                pairs_of_disagreeing_sentences.append((doc, doc_neg))
    
    # Return the list of pairs which sentences show disagreement
    return pairs_of_disagreeing_sentences