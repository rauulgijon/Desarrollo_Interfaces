Ruben Rueda Nieves
-El programa lo primero que hace es inicializar la matriz y pedir el tamaño maximo que pueden tener los barcos, esta tiene una comprobacion para que los barcos no 
puedan ser ni mas pequeños de 2, ni mas grandes que 5 (en este caso 1/3 de la matriz). Despues de esa comprobacion llamamos a un metodo de la clase utilidades
para que nos coloque los barcos, a este metodo le pasamos como parametros el tablero y el tamaño maximo que pueden tener los barcos. Este metodo implementa un bucle con 5
iteraciones, para que coloque los 5 barcos, en estas iteraciones generamos primero la posicion de lo que seria la cabeza del barco o primera posicion de este y que tambien
se elija a que direccion se va a expandir, esto lo hacemos generando otro numeor aleatorio el cual indica la posicion de un vector donde estan las distintas opciones. 
Luego de esas generaciones hacemos las distintas comprobaciones para ver si sus posciones estan ocupadas o si al expandirse se va a salir de la matriz o ocupar la poscion
de algun otro barco, en caso de que esto pase, con una variable booleana declarada a falsa se vuelve a ejecutar un segundo bucle dentro del for, el cual tmbn envuelve todo,
para volver a intentar colocar el barco. En caso de que la colocacion haya sido correcta se pondar el booleano a true y se procedera a la colocacion del siguiente, y asi 
sucesivamente hasta colcarlos todos. Una vez colocado los barcos, llammos a otro metodo de la clase utilidades para que muestre nuestro tablero o matriz.

