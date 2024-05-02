// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
class MySelect {
    #hiddenElementContainer;


    #select;
    
    #containerSelectedItems;
    
    #hiddenInputName;

    constructor(hiddenElementContainer, select, containerSelectedItems, hiddenInputName) {
        this.#hiddenElementContainer = hiddenElementContainer;
        this.#select = select;
        this.#containerSelectedItems = containerSelectedItems;
        this.#hiddenInputName = hiddenInputName;

        this.#Init();
    }

    #Init() {
        this.#select.addEventListener('change', e => {
            let option = this.#select.options[this.#select.selectedIndex];
            if (option.value) {
                const { text, value } = option;
                option.remove();

                this.#addToContainerView(text, value);
            }
        })

    }

    #createHideInput(value) {
        const input = document.createElement('input');
        input.type = 'hidden';
        input.value = value;
        input.name = this.#hiddenInputName;
        input.setAttribute('data-id', value);
        return input;
    }

    #removeHideInput(value) {
        const hideInputToRemove = this.#hiddenElementContainer.querySelector(`[data-id="${value}"]`);
        if (hideInputToRemove) {
            hideInputToRemove.remove();
        }
    }

    #createViewItem(text, value) {

        const button = document.createElement('button');

        button.classList.add('btn', 'btn-primary', 'selectItemView');
        button.innerText = text;
        button.setAttribute('data-id', value);

        button.onclick = e => {
            e.preventDefault();
            const text = e.currentTarget.textContent;
            const value = e.currentTarget.dataset.id;

            button.remove();
            this.#unselect(text, value);
        }

        return button;
    }

    #addToContainerView(text, value) {
        let viewItem = this.#createViewItem(text, value);
        this.#containerSelectedItems.appendChild(viewItem);

        let hideInput = this.#createHideInput(value);
        this.#hiddenElementContainer.appendChild(hideInput);
    }

    #unselect(text, value) {
        this.#removeHideInput(value);

        const option = document.createElement('option');
        option.value = value;
        option.textContent = text;

        this.#select.appendChild(option);

    }
}