## CelaraChallenge

### Spanish

## Introduccion: como pense el ejercicio
Empece creando un metodo que lo que hacia era, palabra por palabra, buscar las ocurrencias en la matriz. Primero buscaba fila por fila cuantas veces encontraba la palabra. Despues armaba strings nuevos con las columnas y buscaba columna por columna las ocurrencias de la palabra. Asi almacenaba en un diccionario la palabra con su cantidad de ocurrencias y despues devolvia las 10 con mas ocurrencias.

Una vez termine este codigo y habiendo probado que funcionaba empece a enfocarme en el performance. Me di cuenta que por cada palabra tenia que recorrer la matriz entera y a mayor cantidad de palabras mas cantidad de procesamiento y mas cantidad de veces tenia que recorrer la matriz.

Al principio pense en paralelizar la busqueda con Tasks para hacerla mas eficiente pero dado que el tamaño maximo de la matriz son 4096 caracteres me parecio que la sobrecarga podria ser mayor al beneficio de performance.

Investigue un poco y encontre una metodo que existe para poder recorrer la matriz una sola vez y buscar todas las palabras simultaneamente. Este metodo se llama Trie y se refiere a una estructura de arbol de prefijos que permite buscar varias palabras simultaneamente. Nunca habia implementado un algoritmo como este asi que tuve que primero comprender como funciona y luego buscar ejemplos que me ayudaran a construir mi solucion. 

Finalmente, luego de un tiempo, termine construyendo una solucion mucho mas optima y que permite una busqueda mucho mas rapida y con menos almacenamiento de memoria.


## Codigo explicado:

El metodo `Find()` lo primero que hace es definir una variable `wordCounts` que es un diccionario que contiene las palabras encontradas y la cantidad de ocurrencias por cada una.
En el primer `Foreach` lo que sucede es que, por cada una de las palabras recibidas, llama al metodo `InsertWord()` y le manda la palabra a insertar. Este metodo lo que hace es inicializar el nodo `Trie` con la palabra que recibe. Al final de la palabra se le agrega un booleano para indicar que ese es el final de la palabra.
Al mismo tiempo se agrega cada una de las palabras al diccionario `wordCounts`, con un valor inicial de cero.

Utilicemos las palabras del codigo a modo de ejemplo, este seria nuestro nodo configurado:

- c - h - i - l - l (fin)
    - o - l - d (fin)
- w - i - n - d (fin)
- r - u - s - h (fin)

Despues, por cada row de nuestra matriz, llamamos al metodo `SearchInRow()` y le pasamos la `row` actual y el `wordCounts`. Este metodo define dos `For` uno dentro del otro. El primero con la variable `i` se utiliza para recorrer la linea, y se asegura de volver a la raiz del `Trie` en cada iteracion. El segundo loop con la variable `j` se utiliza para recorrer el row desde el indice `i` en adelante buscando palabras. De esta forma tomamos la variable `i` como el comienzo de la palabra y podemos iterar con la variable `j` hasta encontrar la palabra completa o el final de la linea.

Dentro del segundo loop comparamos si el caracter existe como hijo del node, si no existe hacemos un break porque significa que en el `Trie` no hay ninguna palabra que comience con el caracter que estamos evaluando. En caso de existir vamos a actualizar el nodo al punto actual para poder buscar desde ese punto el siguiente caracter coincidente.

Antes de seguir con el siguiente caracter verificamos si el nodo corresponde a una palabra completa. Si no lo hace continuamos con el siguiente caracter. En caso de haber completado una palabra se toma el substring que se armo desde nuestro caracter inicial representado por `i` hasta el ultimo caracter coincidente representado por `j`. Sacamos la longitud restandole `i` a `j` y sumandole 1 para incluir a `i` en la longitud de la palabra. Eso nos da como resultado la palabra coincidente. Si encontramos la palabra en nuestro diccionario previamente inicializado, lo que hacemos es sumarle 1 al valor que cuenta las ocurrencias de la palabra.

Esto se repite por cada string que representa una fila dentro de nuestra matriz. Una vez terminado este proceso realizamos la misma busqueda pero en las columnas de la matriz. Para eso primero debemos llamar al metodo `GetColumnWord()`. Este metodo lo que hace es recorrer cada una de las posiciones o columnas de la primer fila de la matriz (considerando que todas las filas tienen la misma longitud como lo dice la consigna). Y por cada una de las columnas va armando los strings recorriendo el resto de los strings de la matriz. Se utiliza el `StringBuilder` para mejorar la performance.

Una vez tenemos las columnas en formato de `row` se hace el mismo proceso que se hizo anteriormente llamando al metodo `SearchInRow()`.

Luego de repetir el recorrido por columnas ya vamos a haber buscado todas las palabras en la matriz y almacenado la cantidad de apariciones de cada palabra en un diccionario. Lo unico que resta por hacer es ordenar las palabras por cantidad de apariciones y devolver las 10 palabras con mas apariciones, filtrando aquellas que no posean ninguna aparicion.

### English
## Introduction: how I thought of the exercise

I started by creating a method that searched for occurrences in the matrix word by word. First, I searched each row to count how many times I found the word. Then, I constructed new strings with the columns and searched column by column for the occurrences of the word. In this way, I stored the word and its count of occurrences in a dictionary and then returned the top 10 words with the most occurrences.

Once I finished this code and verified that it worked, I began to focus on performance. I realized that for each word, I had to traverse the entire matrix, and the greater the number of words, the more processing was required, leading to multiple traversals of the matrix.

At first, I thought of parallelizing the search with Tasks to make it more efficient, but given that the maximum size of the matrix is 4096 characters, I felt the overhead might outweigh the performance benefit.

I did some research and found a method that allows you to traverse the matrix just once and search for all the words simultaneously. This method is called Trie, which refers to a prefix tree structure that enables simultaneous searches for multiple words. I had never implemented an algorithm like this before, so I first had to understand how it works and then look for examples to help me build my solution.

Finally, after some time, I ended up building a much more optimal solution that allows for much faster searches and less memory usage.


## Explained Code:

The method `Find()` first defines a variable `wordCounts`, which is a dictionary containing the found words and their occurrence counts. In the first `Foreach`, for each of the received words, it calls the `InsertWord()` method and sends the word to insert. This method initializes the `Trie` node with the received word. At the end of the word, a boolean is added to indicate that this is the end of the word. At the same time, each word is added to the `wordCounts` dictionary with an initial value of zero.

Let’s use the words from the code as an example; this would be our configured node:

- c - h - i - l - l (end)
    - o - l - d (end)
- w - i - n - d (end)
- r - u - s - h (end)

Then, for each row of our matrix, we call the `SearchInRow()` method and pass the current row and `wordCounts`. This method defines two nested `For` loops. The first loop, with the variable `i`, is used to traverse the line and ensures that we return to the root of the `Trie` at each iteration. The second loop, with the variable `j`, is used to traverse the row from index `i` onward, searching for words. In this way, we take variable `i` as the start of the word and can iterate with variable `j` until we find the complete word or the end of the line.

Within the second loop, we compare whether the character exists as a child of the node. If it does not exist, we break out of the loop because it means there is no word in the `Trie` that starts with the character we are evaluating. If it exists, we update the node to the current point to search for the next matching character from that point onward.

Before continuing with the next character, we verify if the node corresponds to a complete word. If it doesn’t, we continue to the next character. If we have completed a word, we take the substring formed from our starting character represented by `i` to the last matching character represented by `j`. We calculate the length by subtracting `i` from `j` and adding 1 to include `i` in the length of the word. This gives us the matching word. If we find the word in our previously initialized dictionary, we simply add 1 to the value that counts the occurrences of the word.

This process is repeated for each string that represents a row in our matrix. Once this process is completed, we perform the same search but in the columns of the matrix. To do this, we first call the `GetColumnWord()` method. This method traverses each position or column of the first row of the matrix (considering that all rows have the same length as stated in the prompt). For each of the columns, it constructs the strings by traversing the rest of the strings in the matrix. The `StringBuilder` is used to improve performance.

Once we have the columns in the row format, we perform the same process as before by calling the `SearchInRow()` method.

After repeating the traversal for the columns, we will have searched for all the words in the matrix and stored the count of appearances of each word in a dictionary. The only thing left to do is to sort the words by the number of appearances and return the 10 words with the most appearances, filtering out those that have no occurrences.