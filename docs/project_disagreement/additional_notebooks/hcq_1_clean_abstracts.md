---
typora-root-url: ..\assets
---

#### PHILOSOPHY AND HISTORY OF SCIENCE WITH COMPUTATIONAL MEANS

##### PROF. DR. GERD GRAßHOFF 

This markdown file has to be complemented by the corresponding notebook. It is strongly recommended to read the markdown file alongside the notebook.



# 1 Clean Abstracts

The notebook "hcq_1_clean_abstracts.ipynb" aims at creating a **`dataframe`**[^1]

[^1]: If I mention a Python entity, e. g. certain objects or functions, for the first time in a markdown file I will mark it this way: **`PythonEntity`**.

which contains the text of abstracts taken from scientific publications. The dataframe also contains some meta-information about the corresponding abstracts (e. g. the title of the article to which the abstract belongs). The topic of the scientific publications is whether hydroxychloroquine (HCQ) or chloroquine (CQ) is apt as a treatment for COVID-19. In what follows I will comment what is done if you run the notebook.

Most ideas and code of this notebook are taken from a notebook by Prof. Dr. Gerd Graßhoff (even if it is not mentioned).

## 0 Import Libraries

Python libraries are imported that are needed to run the code that follows.



## 1 Read in DIMENSIONS Excel-file as Dataframe

The first thing I did was tho search for scientific publications in the ["Global research on coronavirus disease (COVID-19)"](https://www.who.int/emergencies/diseases/novel-coronavirus-2019/global-research-on-novel-coronavirus-2019-ncov/)-database of the *World Health Organization (WHO)*. The papers should focus on *hydroxychloroquine (HCQ)* or *chloroquine (CQ)* as a possible treatment for COVID-19. I chose some of these publications. A *Digital Object Identifier (DOI)* was given to almost all of them.

In a second step I created an Excel spread sheet storing meta-data according to the respective publications. To reach this aim I used the [DIMENSIONS](https://app.dimensions.ai/discover/publication) database. (Prof. Graßhoff advised me to do so. The code is also from one of his notebooks.) I named the Excel-file `"hcq.xlsx"`. To create it I did the following steps:

* Since I had the DOIs of the publications I selected search option "DOI" and ran a query consisting of the DOIs. (One of my documents had no DOI, so I chose search option "Title and abstract" and searched for some sentences of this publications's "SUMMARY" section.) 
* When the DIMENSIONS database showed the search results, I clicked on "Save/Export" (on the right next to the search bar). I chose to export the results as an Excel-file. 

Third, I used the **`function `** `read_excel()` from the library `pandas` ("`pd`") to create a dataframe from the Excel spread sheet `hcq.xlsx` . I stored the dataframe in the variable `df_hcq`:

![`df_hcq.head(3)`](/df_hcq_head_first.PNG)

 *`df_hcq.head(3)`*

In the dataframe there is a row for each publication. Each column represents a type of meta-information about a publication. As you can see there are a lot of meta-data (27 columns [!]).

**Note:** Within the `pd.read_excel()` function I did not use `"hcq.xlsx"` as argument for the filename parameter. But I used `"../data/hcq.xlsx"` instead. To understand that **`string`** it might help to look at the location of the current notebook (i. e. "hcq_1_clean_abstracts.ipynb") within the structure of the directory:

* .ipynb_checkpoints
* additional_notebooks
  - .ipynb_checkpoints
  - hcq_1_clean_abstracts.ipynb
  - [...]
* assets
* data
  - hcq.xlsx
  - [...]
* hcq.ipynb
* [...]

The notebook "hcq_1_clean_abstracts.ipynb" is in the directory "additional_notebooks". But the excel spread sheet "hcq.xlsx" is within the directory "data". Neither of them is in the directory "additional_notebooks". The directories "additional_notebooks" and "data" are of the same level. Neither is a sub-directory of the other. They are two parallel branches of the same tree structure, so to speak. `../` tells Python to go from "additional_notebooks" one level up. `data/` tells Python to change directory to "data" and to access "hcq.xlsx" there.

## 2 Exploit Websites

Most of the code in this section is from one of Prof. Graßhoff's notebooks, among other things: the **`list`** `pubkeys` and the function `getmeta()`. 

The dataframe `df_hcq` has a column called `"Dimensions URL"`. The entries in this column are URLs leading to webpages that contain meta-data about a certain publication and the abstract belonging to that publication. Let's look for example at the first entry in the column `Dimensions URL`. To retrieve the first row in this column run this code: 

```python
df_hcq["Dimensions URL"].iloc[0]
```

As a result you get the following URL (as a string):

```python
'https://app.dimensions.ai/details/publication/pub.1126880632'
```



This URL leads to the following webpage:

![dimensions_webpage](/dimensions_webpage.PNG )*`https://app.dimensions.ai/details/publication/pub.1126880632`*

As you can see there is an abstract on this webpage.

On this basis we do the following steps:

* We define the function `getmeta()`: This function opens a URL extracts some meta-data about a publication from that webpage (e.g. abstract). It also creates a **`dictionary`**-object (**`dict`**, for short), in this case we name it `dicpub`. The meta-data are stored in that dictionary and the  dictionary is returned.

* With

  ```python
  df_hcq.apply(getmeta, axis=1)
  ```

  we apply `getmeta()` to every single row of the dataframe `df_hcq`. We store the result in the variable `df_hcq["seldict"]` thereby creating an additional column of dataframe `df_hcq`, called `seldict`. Each row in that column contains a dictionary. Such a dictionary encompasses, among other things, an abstract of the respective publication. See for example the first entry of column `seldict` (`df_hcq["seldict"].iloc[0]`). (Please compare it with the abstract on the webpage above.) 
  
  ![seldict_example](/seldict_example.PNG)*`df_hcq["seldict"].iloc[0]`*



## 3 Create New Dataframe with Column "abstract"

The code is from one of Prof. Graßhoff's notebooks.

As up to now we have a dataframe-object that contains meta-data about scientific publications, ordered in a tabular form. One column of that dataframe (i. e. `seldict`) even encompasses abstracts. But the abstracts are not accessed easily. In order to help with that we create a new dataframe that has a column "abstract" in which abstracts are stored. It is done in the following steps:

Code:

```python
accessible_abstracts = []
```

```python
# Define function newpub()
def newpub(x):
    di = dict()
    d = x["seldict"]
    di["Publication ID"] = x["Publication ID"]
    di["DOI"] = x["DOI"]
    di["title"] = x["Title"]
    di["authors"] = d["authors_full"]
    di["publisher"] = d["publisher"]
    di["source"] = x["Source title"]
    di["aff_org_name"] = d["aff_org_name"]
    di["aff_country"] = d["aff_country_name"]
    di["pub_date"] = d["pub_date"]
    di["abstract"] = d["abstract"]
    di["openaccess"] = d["open_access"]
    di["di_URL"] = x["Dimensions URL"]
    accessible_abstracts.append(di)
```

```python
df_hcq.apply(lambda x:newpub(x), axis=1)
```



* We create a list and we name it `accessible_abstracts`. At the beginning `accessible_abstracts` is empty, but it shall be filled with dictionary-objects.

* We define a function `newpub()`.  It creates an empty dictionary `di` . Then it takes a row of a dataframe at a time and stores the information in `di`. What is important here is, that `newpub()` unwraps the abstracts from the dict-objects that are stored in `df_hcq["seldict"]`. It adds the dict `di` to `accessible_abstracts`.

* To add dictionary-objects to `accessible_abstracts`, we apply `newpub()` to each row of `df_hcq`: `df_hcq.apply(lambda x:newpub(x), axis=1)`.

* `accessible_abstracts` is the base for creating a new dataframe: `df_hcq_Lit`. We create it with panda's function `DataFrame()`. The code is as follows: 

  ```python
  df_hcq_Lit = pd.DataFrame(accessible_abstracts)
  ```

`df_hcq_Lit` has a column `abstract`, in which abstracts are stored. These are now easier accessible. So we end up with a dataframe. That has meta-data about scientific publications. And as a valuable feature they have for the publications the corresponding abstracts.



## 4 Clean Abstracts

But, still the dataframe has to be tidied up a bit.  There is a lot of meta-data that might be good to have at some point, but it is not needed for the current purpose. Furthermore there are some issues with the abstracts we have to deal with.



### 4.1 Information Reduction

We take only the columns `Publication ID`, `title` and `abstract` of `df_hcq_Lit` and call that reduced dataframe `df_HCQ`. Calling `df_HCQ.head()` we get the first five rows. It looks like this:

![df_HCQ_head](/df_HCQ_head.png)*df_HCQ.head()*



### 4.2 Remove Rows Containing Missing Abstracts

We now have a dataframe that contains the title, publication ID and abstract for all selected publications. As you can see in the image above, there are abstracts missing in some rows. But the notebook does not register the missing abstracts as missing: Of 19 entries in the column `abstract` 19 are counted as "non-null" (see the next image below). Instead it counts empty strings (`''`) as a kind of abstract - an empty but still existent abstract. 

![df_HCQ_info_empty_abstracts](/df_HCQ_info_empty_abstracts.png)

*`df_HCQ.info() with "empty abstracts"`*

It is the next step to get rid of those rows that contain empty strings in the `abstract` column:

* Remove "empty abstracts":

  Code:

  ```python
  drop_list = []
  for i in range(len(df_HCQ)):
      if df_HCQ["abstract"].iloc[i] == '':
          drop_list.append(i)
          
  df_HCQ_dropped = df_HCQ.drop(drop_list)
  ```

  

  - An empty list-object named `drop_list` is created. 
  - Every row of `df_HCQ` has a unique number. These row numbers are the *index* of `df_HCQ`.  We make a `for`-loop which iterates over all index numbers. For the current row number in the index, the column `abstract` in that row is accessed. The string in that dataframe field is compared to a certain string we are looking for (i. e. `''`) . If the string is equal to `''` , i. e. if there is an empty string instead of an abstract, then the index number is added to `drop_list`. So `drop_list` is a list of index numbers that contains the numbers of those rows which shall be removed.
  - We call the dataframe-**`method`** `drop()` on `df_HCQ` with `drop_list`  as argument and get a dataframe which is the same as `df_HCQ` , but the rows with empty strings are removed. We store that new dataframe in the variable `df_HCQ_dropped`.

- Because we removed some rows (i. e. some numbers from the index) the rows of `df_HCQ_dropped` not numbered in order. To have an index consisting of running numbers we make new index for `df_HCQ_dropped`:

  ```python
  df_HCQ_dropped.index = [i for i in range(len(df_HCQ_dropped))]
  ```

We now have a dataframe that has only with 17 rows which are numbered by running numbers.



### 4.3 Remove Special Characters

In the remaining rows of column `abstract` there are only strings that form the text of an abstract. Since these strings are gained from html-code the strings might contain certain characters that are not part of the text and that are not usually printed but serve certain purposes.  Take for example the last sentence of the second abstract in column `abstract` of `df_HCQ_dropped`. (You can access that abstract-string with: `df_HCQ_dropped["abstract"].iloc[1]`):

```python
'\nFUNDING: William Harvey Distinguished Chair in Advanced Cardiovascular Medicine at Brigham and Women&#x27;s Hospital.'
```

There are two special strings in this example: `\n` and `&#x27;`.  The string `\n` is usually not printed, but makes a linebreak. And the string `&#x27;` makes a simple `'`, so that the text should read `Women's Hospital` instead of `Women&#x27;s Hospital`. 

Because such special characters and strings might disturb the later natural language processing (and can make a weird spelling of the text) we want to remove them. I did it in the following steps:

* I manually browsed through the strings (abstracts) in  column `abstract` of `df_HCQ_dropped` and collected potentially disturbing characters and strings.

* We define the function `clean_abstract()`. It uses the `sub()` function from `re` (*regular expressions*) which replaces all occurrence of some sub-string within some string with another sub-string. We define which of the disturbing characters and (sub-)strings should be replaced by another (sub-)string. For example we define that every occurrence of `&#x27;` in an abstract should be replaced by  `'` (see `c4 = re.sub("&#x27;", "'", c3)` ) . If you apply `clean_abstract()` on a single abstract it removes all disturbing strings from an abstract and returns an abstract which is now cleaned in such a way.

  ```python
  def clean_abstract(abstract):
      c1 = re.sub("\u2009", " ", abstract)
      c2 = re.sub("\n", " ", c1)
      c3 = re.sub("\u2008", " ", c2)
      c4 = re.sub("&#x27;", "'", c3)
      c5 = re.sub("#", " ", c4)
      c6 = re.sub("\xa0", " ", c5)
      c7 = re.sub("&quot;", " ", c6)
      c8 = re.sub("& s", "'", c7)
      c9 = re.sub("##", " ", c8)
      c10 = re.sub("###", " ", c9)
      c11 = re.sub("&gt;", ">", c10)
      
      cleaned_abstract = c11
      return cleaned_abstract
  ```

  

* We apply  `clean_abstract()`  on each abstract in  column `abstract` of dataframe `df_HCQ_dropped` (row by row). Thus we create a new column which we give the name `abstract_clean`.

  ```python
  df_HCQ_dropped["abstract_clean"] = df_HCQ_dropped["abstract"].apply(clean_abstract)
  ```

* The dataframe `df_HCQ_dropped` has now the four following columns:

  - `Publication ID`
  - `title`
  - `abstract`
  - `abstract_clean`

* If we look at the cleaned example sentence from above (see `df_HCQ_dropped["abstract_clean"].iloc[1]`, last sentence) it now looks like this:

  ```python
  "FUNDING: William Harvey Distinguished Chair in Advanced Cardiovascular Medicine at Brigham and Women's Hospital."
  ```

  It now looks nice and clean.

* Since we might not need the column `abstract` any more we redefine `df_HCQ_dropped` so that it has only the three remaining columns:

  ```python
  df_HCQ_dropped = df_HCQ_dropped[["Publication ID", "title", "abstract_clean"]]
  ```



## 5 Store Dataframe in a .json-file

As a last thing we save the results we have reached so far.  We export the dataframe`df_HCQ_dropped` as a .json-file. Therefore we use a dataframe-method of `pandas` which is called `to_json()`. Let us call our file `HCQ_clean_abstracts.json`.  We store that file in the directory `data`:

```python
df_HCQ_dropped.to_json("../data/HCQ_clean_abstracts.json")
```



