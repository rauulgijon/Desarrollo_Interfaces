/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 */

package com.mycompany.mavenproject1;

/**
 *
 * @author Raul Gijon Sanchez - Maroto
 * @param <T>
 */
/*
 * Clase genérica Nodo para listas enlazadas
 * 
 * @param <T> Tipo de dato que contendrá el nodo
 */

public class Nodo <T> {
    
    // ATRIBUTOS
    private T elemento;          // Dato que almacena el nodo
    private Nodo<T> siguiente;   // Referencia al siguiente nodo
    
    // CONSTRUCTOR
    /**
     * Crea un nodo con un elemento
     * 
     * @param elemento Dato que se guardará en el nodo
     */
     public Nodo(T elemento) {
        this.elemento = elemento;
        this.siguiente = null;
    }
     
    /**
     * Crea un nodo con un elemento y la referencia al siguiente nodo.
     * 
     * @param elemento Dato que se guardará en el nodo
     * @param siguiente Referencia al siguiente nodo (null si es el último)
     */
    public Nodo(T elemento, Nodo<T> siguiente){
        this.elemento = elemento;
        this.siguiente = siguiente;
    }
    
    // GETTERS
    /**
     * Devuelve el elemento almacenado en el nodo.
     * @return elemento del nodo
     */
    public T getElemento(){
        return elemento;
    }
    
    /**
     * Devuelve el nodo siguiente en la lista.
     * @return siguiente nodo
     */
    public Nodo<T> getSiguiente(){
        return siguiente;
    }
    
    // SETTERS
    /**
     * Asigna un nuevo valor al elemento del nodo.
     * @param elemento nuevo dato a almacenar
     */
    public void setElemento(T elemento){
        this.elemento = elemento;
    }
    
    /**
     * Asigna una nueva referencia al siguiente nodo.
     * @param siguiente nuevo nodo siguiente
     */
    public void setSiguiente(Nodo<T> siguiente){
        this.siguiente = siguiente;
    }
    
    // MÉTODOS
    /**
     * Devuelve una representación en cadena del nodo (su elemento).
     * @return String con el elemento
     */
    @Override
    public String toString(){
        return elemento + "\n";
    }
}


