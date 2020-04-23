#### PHILOSOPHY AND HISTORY OF SCIENCE WITH COMPUTATIONAL MEANS

##### PROF. DR. GERD GRAßHOFF 



# Notebook Environments 

There are many text editors for writing Python code, but in this seminar, we will be working with the most popular notebook environment, which is Juypter. Notebook environments are suitable for learning because it is possible to see multiple inputs and outputs next to each other in one space so that the actor does not have to write a large script first. In this way, we can just quickly type something and see if the output works. Notebooks use the so-called "cells" that are blocks of code that you later run.

Another essential feature of notebook environments is that they support markdown notes (standard text for titles and explanations), visualizations, etcetera.



## Jupyter Notebooks

If we launch Jupyter Notebook from the Anaconda Navigator, the browser will open up to "localhost:8888/tree" where you can see the files of your computer. Only from here, we can open up the Jupyter-files. Jupyter Notebooks have a particular format, which is an extension ".ipynb." 

![homejupy](C:\Users\User\Desktop\homejupy.JPG)

- To open a notebook, click on "New" &rightarrow; "Python 3."

This is how a notebook looks:

![newnb](C:\Users\User\Desktop\newnb.JPG)

Save the Jupyter-files in the current directory (by default, this will happen). To check the current directory type in the cell:

```
pwd
```

- To run the cell, click on the button "Run" in the toolbar above or press "Shift-Enter."

From the prompt/terminal, it is possible to change the directory and then open a notebook typing the following:

```
cd Desktop
```

```
jupyter notebook
```



## Plain Text

- To change the title, go to the tab "Untitled" and write a new name.

- To write plain text in a cell, go to the toolbar where it says "Code" and select "Markdown." So it will no longer be a code cell, but one for text.
- For headings, choose the size of the headings putting a "#" before the text:

```
# heading 1
## heading 2
### heading 3
#### heading 4
##### heading 5
###### heading 6
```



## Worth Mentioning

- To add a cell click the plus in the toolbar or press "Esc-B"
- To see a list of all the keyboard shortcuts, go to "Help" &rightarrow; "Keyboard Shortcuts"
- If the browser does not automatically open from the Anaconda Navigator, manually open a browser and type in: localhost:8888
- Jupyter Notebook operates inside the browser as a graphical interface, but it does not need an Internet connection. 

