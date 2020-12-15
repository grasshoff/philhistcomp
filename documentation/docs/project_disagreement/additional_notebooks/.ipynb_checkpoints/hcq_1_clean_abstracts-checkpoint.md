# Remarks to "hcq_1_clean_abstracts.ipynb"

The notebook "hcq_1_clean_abstracts" aims at creating a dataframe which contains the text of abstracts taken from scientific publications. The dataframe also contains some meta-information about the corresponding abstracts (e.g. the title of the article to which the abstract belongs). The topic of the scientific publications is whether hydroxychloroquine (HCQ) or chloroquine (CQ) is apt as a treatment for COVID-19. In what follows I will comment what is done if you run the notebook.

Most ideas and code of this notebook was taken from a notebook by Prof. Dr. Gerd Graßhoff.



## 0 Import Libraries

Python libraries are imported that are needed to run the code that follows.



## 1 Read in Excel-file as Dataframe

The first thing I did was tho search for scientific publications in the ["Global research on coronavirus disease (COVID-19)"](https://www.who.int/emergencies/diseases/novel-coronavirus-2019/global-research-on-novel-coronavirus-2019-ncov/)-database of the *World Health Organization (WHO)*. The papers should focus on *hydroxychloroquine (HCQ)* or *chloroquine (CQ)* as a possible treatment for COVID-19. I chose some of these publications. A *Digital Object Identifier (DOI)* was given to almost all of them.

In a second step I created an Excel spread sheet storing meta-data according to the respective publications. To reach this aim I used the [DIMENSIONS](https://app.dimensions.ai/discover/publication) database. (Prof. Graßhoff advised me to do so. The code is also from one of his notebooks.) I named the Excel-file "`hcq.xlsx`". To create it I did the following steps:

* Since I had the DOIs of the publications I selected search option "DOI" and ran a query consisting of the DOIs. (One of my documents had no DOI, so I chose search option "Title and abstract" and searched for some sentences of this publications's "SUMMARY" section.) 
* When the DIMENSIONS database showed the search results, I clicked on "Save/Export" (on the right next to the search bar). I chose to export the results as an Excel-file. 

Third, I used the function `read_excel()` from the library `pandas` ("`pd`") to create a dataframe from the Excel spread sheet `hcq.xlsx` . I stored the dataframe in the variable `df_hcq`:

![`df_hcq.head(3)`](C:\Users\Tobias_lokal\Documents\SoSe_2020\disagreement_publication\assets\df_HCQ_head.PNG)

In the dataframe there is a row for each publication. Each column represents a type of meta-information about a publication. As you can see there are a lot of meta-data (27 colums [!]).