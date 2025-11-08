# 🎓 Proiect Facultate: Student Life Simulator

**Obiectiv:** Crearea unui joc de simulare 2D în Unity care reproduce provocările și echilibrul dintre viața academică și cea personală a unui student.

**Echipa de Dezvoltare:**
* Pichler Gabriel
* Podean Beniamin

---

## I. Conceptul Jocului 🎯

Jocul se bazează pe gestionarea resurselor (statistici) pentru a menține jocul functional și pentru a obține note bune la examene.

### 1. Statistici de Bază (Resurse)

Jucătorul trebuie să echilibreze următoarele variabile, care scad în timp și/sau în funcție de acțiuni:

| Statistică | Reprezintă | Consecințe ale valorii scăzute |
| :--- | :--- | :--- |
| **Oboseală** | Nevoia de somn | Viteză de învățare redusă, șanse crescute de eșec la acțiuni. |
| **Foame** | Nevoia de a mânca | Scăderea rapidă a celorlalte statistici. |
| **Nevoia Socială** | Sănătatea mentală, Starea de spirit | Depresie, lipsă de motivație (scădere a progresului de învățare). |
| **Bani** | Resursă de cheltuit | Nu poți cumpăra mâncare sau obiecte de confort. |

### 2. Statistici de Progres

| Statistică | Reprezintă | Cum crește |
| :--- | :--- | :--- |
| **Progres [Materie X]** | Nivelul de pregătire la o anumită materie. | Acțiunea **Studiu** targetată pe materia X. |
| **Note Finale** | Media academică actuală. | Recompensă din finalizarea cu succes a Progresului de învățare la o materie (Examen). |

---

## II. Tehnologie și Setup 🛠️

* **Motor de Joc:** Unity [Versiunea X.X.X]
* **Limbaj de Programare:** C#
* **Stil Vizual:** 2D (Recomandat: Top-Down sau Fixed Screen)
* **Version Control:** Git (Repository: [Link către GitHub/GitLab])

### Ghid de Setup Git (Obligatoriu pentru Echipa)

1.  **Clonați Repository-ul:** `git clone [URL-ul Repo-ului]`
2.  **Configurați Unity:** În **Edit > Project Settings > Editor**, asigurați-vă că **Asset Serialization Mode** este setat pe **Force Text**.
3.  **Fișierul `.gitignore`** este deja configurat pentru a ignora fișierele mari și temporare (`Library/`, `Temp/`, etc.). **Nu le comitați!**
4.  **Workflow Recomandat:** Lucrați pe *branches* separate pentru funcționalități majore, și faceți *merge* în *main* doar după ce codul a fost testat.

---

## III. Faze de Dezvoltare și Sarcini 🗓️

### Faza 1: Prototip (Sprint 1)

* [ ] Crearea scenei de bază (camera de cămin/biroul).
* [ ] Implementarea tuturor celor **6 Statistici** ca variabile `float`.
* [ ] Crearea **UI-ului** (Panoul de Stats) și legarea lor la variabile (folosind *Sliders*).
* [ ] Implementarea **Mecanicii Timpului** (variabilele scad în timp).

### Faza 2: Logica de Bază (Sprint 2)

* [ ] Implementarea **Acțiunilor de Bază** (`Dormit`, `Mâncat`, `Studiu`,'Petrece').
* [ ] Implementarea **Sistemului de Bani** (câștig/cheltuire).
* [ ] Conectarea acțiunilor la statistici (de exemplu, `Dormit` crește `Oboseala`, scade `Foamea`).
* [ ] Funcționalitate minimă pentru **Progresul de Învățare** (o singură materie).

### Faza 3: Conținut și Polish (Sprint 3)

* [ ] Adăugarea interacțiunilor sociale (`Ieșit în Oraș`, `Vorbit la telefon`).
* [ ] Adăugarea **Evenimentelor Aleatorii** (Ex: "Ai luat bursă", "Ai răcit").
* [ ] Implementarea **Examenului/Colocviului** (calcularea Notei Finale pe baza Progresului de Învățare).
* [ ] Finalizarea Art-ului 2D (sprite-uri, fundaluri).
* [ ] Testare și Bug Fixing.

---


