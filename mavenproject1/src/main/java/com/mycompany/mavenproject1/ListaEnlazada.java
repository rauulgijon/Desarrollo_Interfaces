/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */

package com.mycompany.mavenproject1;

/**
 *
 * @author Raul Gijon Sanchez - Maroto
 * Created on 16 sept 2025
 */
public class ListaEnlazada<T> {
    
    // Atributos
    private Nodo<T> primero;

    /**
     * Constructor por defecto
     */
    public ListaEnlazada() {
        listaVacia();
    }
    
    /**
     * Vacia la lista
     */
    private void listaVacia() {
        primero = null;
    }

    /**
     * Indica si la lista esta vacia o no
     * @return True = esta vacia
     */
    public boolean estaVacia() {
        return primero == null;
    }

    /**
     * Inserta un objeto al principio de la lista
     * @param t Dato insertado
     */
    public void insertarPrimero(T t) {
        Nodo<T> nuevo = new Nodo<>(t);
        if (!estaVacia()) {
            // Si no está vacía, el primero actual pasa a ser
            // el siguiente de nuestro nuevo nodo
            nuevo.setSiguiente(primero);
        }
        // El primero apunta al nodo nuevo
        primero = nuevo;
    }

    /**
     * Inserta al final de la lista un objeto
     * @param t Dato insertado
     */
    public void insertarUltimo(T t) {
        Nodo<T> aux = new Nodo<>(t);
        Nodo<T> rec_aux;
        if (estaVacia()) {
            insertarPrimero(t);
        } else {
            rec_aux = primero;
            // Buscamos el último nodo
            while (rec_aux.getSiguiente() != null) {
                rec_aux = rec_aux.getSiguiente();
            }
            // Actualizamos el siguiente del último
            rec_aux.setSiguiente(aux);
        }
    }

    /**
     * Quita el primer elemento de la lista
     */
    public void quitarPrimero() {
        if (!estaVacia()) {
            Nodo<T> aux = primero;
            primero = primero.getSiguiente();
            aux = null; // Lo marcamos para el recolector de basura
        }
    }

    /**
     * Quita el último elemento de la lista
     */
    public void quitarUltimo() {
        Nodo<T> aux = primero;
        if (aux.getSiguiente() == null) {
            // Aquí entra si la lista tiene un solo elemento
            listaVacia();
        }
        if (!estaVacia()) {
            aux = primero;
            // Buscamos el penúltimo, por eso hay dos getSiguiente()
            while (aux.getSiguiente().getSiguiente() != null) {
                aux = aux.getSiguiente();
            }
            // Marcamos el siguiente del antepenúltimo como nulo, eliminando el último
            aux.setSiguiente(null);
        }
    }

    /**
     * Devuelve el último elemento de la lista
     * @return Último elemento
     */
    public T devolverUltimo() {
        T elemen = null;
        Nodo<T> aux;
        if (!estaVacia()) {
            aux = primero;
            // Recorremos
            while (aux.getSiguiente() != null) {
                aux = aux.getSiguiente();
            }
            elemen = aux.getElemento();
        }
        return elemen;
    }

    /**
     * Devuelve el primer elemento de la lista
     * @return Primer elemento, null si esta vacia
     */
    public T devolverPrimero() {
        T elemen = null;
        if (!estaVacia()) {
            elemen = primero.getElemento();
        }
        return elemen;
    }

    /**
     * Devuelve el número de elementos de la lista
     * @return Número de elementos
     */
    public int cuantosElementos() {
        Nodo<T> aux = primero;
        int numElementos = 0;
        // Recorremos
        while (aux != null) {
            numElementos++;
            aux = aux.getSiguiente();
        }
        return numElementos;
    }

    /**
     * Devuelve el dato del nodo en la posición pos
     * @param pos
     * @return dato del nodo en la posicion indicada
     */
    public T devolverDato(int pos) {
        Nodo<T> aux = primero;
        int cont = 0;
        T dato = null;
        if (pos < 0 || pos >= cuantosElementos()) {
            System.out.println("La posicion insertada no es correcta");
        } else {
            // Recorremos
            while (aux != null) {
                if (pos == cont) {
                    dato = aux.getElemento();
                }
                aux = aux.getSiguiente();
                cont++;
            }
        }
        return dato;
    }

    /**
     * Devuelve el nodo de la posicion indicada
     * @param pos
     * @return Nodo de la posicion indicada
     */
    public Nodo<T> devolverNodo(int pos) {
        Nodo<T> aux = primero;
        int cont = 0;
        if (pos < 0 || pos >= cuantosElementos()) {
            System.out.println("La posicion insertada no es correcta");
        } else {
            // Recorremos
            while (aux != null) {
                if (pos == cont) {
                    return aux;
                }
                aux = aux.getSiguiente();
                cont++;
            }
        }
        return aux;
    }

    /**
     * Inserta un nuevo nodo en la posicion indicada con su dato
     * @param pos
     * @param dato
     */
    public void introducirDato(int pos, T dato) {
        Nodo<T> aux = primero;
        Nodo<T> auxDato = null; 
        Nodo<T> anterior = primero; 
        int contador = 0;
        if (pos < 0 || pos > cuantosElementos()) {
            System.out.println("La posicion insertada no es correcta");
        } else {
            if (pos == 0) {
                insertarPrimero(dato);
            } else if (pos == cuantosElementos()) {
                insertarUltimo(dato);
            } else {
                // Recorremos
                while (aux != null) {
                    if (pos == contador) {
                        auxDato = new Nodo<>(dato, aux);
                        anterior.setSiguiente(auxDato);
                    }
                    anterior = aux;
                    contador++;
                    aux = aux.getSiguiente();
                }
            }
        }
    }

    /**
     * Modifica el dato indicado en el nodo de la posicion indicada
     * @param pos
     * @param dato
     */
    public void modificarDato(int pos, T dato) {
        Nodo<T> aux = primero;
        int cont = 0;
        if (pos < 0 || pos >= cuantosElementos()) {
            System.out.println("La posicion insertada no es correcta");
        } else {
            // Recorremos
            while (aux != null) {
                if (pos == cont) {
                    aux.setElemento(dato);
                }
                cont++;
                aux = aux.getSiguiente();
            }
        }
    }

    /**
     * Borra un elemento de la lista
     * @param pos Posición de la lista que queremos borrar
     */
    public void borraPosicion(int pos) {
        Nodo<T> aux = primero;
        Nodo<T> anterior = null;
        int contador = 0;
        if (pos < 0 || pos >= cuantosElementos()) {
            System.out.println("La posicion insertada no es correcta");
        } else {
            while (aux != null) {
                if (pos == contador) {
                    if (anterior == null) {
                        primero = primero.getSiguiente();
                    } else {
                        anterior.setSiguiente(aux.getSiguiente());
                    }
                    aux = null;
                } else {
                    anterior = aux;
                    aux = aux.getSiguiente();
                    contador++;
                }
            }
        }
    }

    /**
     * Devuelve el primer el elemento y lo borra de la lista
     * @return Primer elemento
     */
    public T devolverYBorrarPrimero() {
        T dato = devolverPrimero();
        quitarPrimero();
        return dato;
    }

    /**
     * Indica la posición del primer dato que se encuentre
     * @param t dato buscado
     * @return Posición del dato buscado, -1 si no se encuentra o esta vacia
     */
    public int indexOf(T t) {
        Nodo<T> aux = primero;
        if (estaVacia()) {
            return -1;
        } else {
            int contador = 0;
            boolean encontrado = false;
            while (aux != null && !encontrado) {
                if (t.equals(aux.getElemento())) {
                    encontrado = true;
                } else {
                    contador++;
                    aux = aux.getSiguiente();
                }
            }
            return encontrado ? contador : -1;
        }
    }

    /**
     * Indica la posición del primer dato desde la posicion indicada
     * @param t dato buscado
     * @param pos
     * @return Posición del dato buscado, -1 si no se encuentra o esta vacia
     */
    public int indexOf(T t, int pos) {
        if (estaVacia()) {
            return -1;
        } else {
            int contador = pos;
            boolean encontrado = false;
            Nodo<T> aux = devolverNodo(pos);
            while (aux != null && !encontrado) {
                if (t.equals(aux.getElemento())) {
                    encontrado = true;
                } else {
                    contador++;
                    aux = aux.getSiguiente();
                }
            }
            return encontrado ? contador : -1;
        }
    }

    /**
     * Indica si un dato existe en la lista
     * @param t Dato a comprobar
     * @return Si el dato existe, devuelve true
     */
    public boolean datoExistente(T t) {
        boolean existe = false;
        Nodo<T> aux = primero;
        while (aux != null && !existe) {
            if (aux.getElemento().equals(t)) {
                existe = true;
            }
            aux = aux.getSiguiente();
        }
        return existe;
    }

    /**
     * Muestra el contenido de la lista
     */
    public void mostrar() {
        System.out.println("Contenido de la lista");
        System.out.println("---------------------");
        Nodo<T> aux = primero;
        while (aux != null) {
            System.out.println(aux.getElemento());
            aux = aux.getSiguiente();
        }
    }

    /**
     * Devuelve el contenido de la lista en un String
     * @return contenido de la lista
     */
    @Override
    public String toString() {
        String contenido = "";
        Nodo<T> aux = primero;
        while (aux != null) {
            contenido += aux.getElemento() + "\n";
            aux = aux.getSiguiente();
        }
        return contenido;
    }
}
