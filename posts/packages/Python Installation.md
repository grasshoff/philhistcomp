#### PHILOSOPHY AND HISTORY OF SCIENCE WITH COMPUTATIONAL MEANS

##### PROF. DR. GERD GRAßHOFF 



# Python

Python wurde bei Guido Van Rossum in 1991 entwickelt und es ist eine sehr beliebte Programmiersprache, weil es einfach zu lernen ist, denn die Syntax ist viel einfacher und man braucht weniger Code als andere Programmiersprachen. Außer die Standardbibliotheken, gibt es ganz viele Open Source Bibliotheken für zusätzliche Frameworks.



## Installation

Wir müssen Python durch die Anaconda-Distribution installieren. Anaconda enthält nützliche Bibliotheken und ganz wichtig: Jupyter, das Notebook-System, das wir im Verlauf des Seminares verwenden werden. 

- Um mit der Installation zu beginnen:

  https://www.anaconda.com/distribution/

Dort sind die Download-Links für die MacOS, Linux und Windows. Je nach Computer laden Sie entweder das 64-Bit- oder 32-Bit-Installationsprogrammm herunter. (Die meisten Computer sind 64-Bit.) Folgen Sie den Installationsprozess Schritt für Schritt.

- Mehr Information hier (genaue Anweisungen):

  https://docs.anaconda.com/anaconda/install/

- Windows:

  https://docs.anaconda.com/anaconda/install/windows/

- MacOS:

  https://docs.anaconda.com/anaconda/install/mac-os/

 -  Linux:

   https://docs.anaconda.com/anaconda/install/linux/b

- Es gibt auch eine kleinere Version, die "Miniconda" heißt, für diejenigen, die nicht viel Platz auf dem Computer haben:

  https://docs.conda.io/en/latest/miniconda.html

Wenn Sie über den Speicherplatz (5 GB) verfügen, empfehlen wird jedoch die vollständige Anaconda-Installation. 

- Wenn Sie Anaconda erfolgreich heruntergeladen haben und die Installationsschritte durchlaufen haben, sollten Sie in der Lage sein, "Anaconda Navigator" zu finden. Diese ist eine Desktop-App, mit der Sie schnell Jupyter Notebooks starten können.
- Also bestätigen Sie Ihre Installation, indem Sie auf "Anaconda Navigator" klicken.
- Die Umgebung dieses Seminars ist "Jupyter Notebook":

![jupyter](C:\Users\User\Documents\GitHub\philhistcomp\posts\packages\assets\jupyter.JPG)



## Kommandozeile

Die Befehle sind anders in MacOS, Linux und Windows. 

Tipp: Wenn Sie durch die Verzeichnisse navigieren möchten, nutzen Sie "cd D" und drücken Sie auf die Tabulatortaste.



### Windows

Schauen wir zuerst einige Windows-Befehlszeilenoperationen

Ihr müsst "Eingabeaufforderung" suchen, oder auf Englisch "Command Promt"

- Dann könnt ihr Folgendes eingeben, es steht für "current directory":

  ```
  cd
  ```

- Die nächste Eingabe steht für "directory" und zeigt den Inhalt des Verzeichnisses:

  ```
  dir
  ```

- Um das Verzeichnis zu ändern, geben Sie "cd NameDesVerzeichnisses", zum Beispiel:

  ```
  cd Desktop
  ```

- Um zurück zu gehen:

  ```
  cd ..
  ```

- Mit der nächsten Eingabe, wird alles gelöscht ("clear screen").

  ```
  cls
  ```

  

### MacOs & Linux

In MacOS und Linux müsst ihr "Terminal" suchen. 

- Um zu sehen, wo wir uns gerade befinden, Folgendes eingeben:

  ```
  pwd
  ```

- Mit der nächsten Eingabe, könnt ihr eine Liste mit den Dateien dieses Verzeichnisses sehen:

  ```
  ls
  ```

- Um das Verzeichnis zu ändern, geben Sie "cd NameDesVerzeichnisses", zum Beispiel:

  ```
  cd Desktop
  ```

- Um zurück zu gehen:

  ```
  cd ..
  ```

- Um alles zu löschen:

  ```
  clear
  ```

  

## Pakete

- Um den aktuellen Status der Umgebung zu sehen, geben Sie in die Kommandozeile Folgendes ein:

```
pip freeze
```

- Suchen Sie in der Liste nach:
  - [ ] nltk
  - [ ] pandas
  - [ ] re
  - [ ] scikitlearn
  - [ ] spacy
  
- Wenn Sie alle Pakete finden, dann sind Sie fertig mit dem Setup. 

- Wenn Sie die Pakete nicht finden, öffnen Sie die Kommandozeile. Gegebenenfalls installieren Sie wie folgt:

   - nltk

     ```
     pip install nltk
     ```

   - pandas

     ```
     pip install pandas
     ```

   - re

     ```
     pip install regex
     ```

   - scikitlearn

     ```
     pip install scikit-learn
     ```

   - spacy

     ```
     pip install spacy
     ```

     