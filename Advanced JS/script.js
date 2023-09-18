class Book{
    constructor(id,title,author,isbn){
        this.id = id;
        this.title = title;
        this.author = author;
        this.isbn = isbn;
    }
}

class UI{
    addBookToList(book){
        bookLists.push(book);
        localStorage.setItem("list",JSON.stringify(bookLists)); 
        UI.displayBookList(); 
    }

    static displayBookList(){
        const list = document.getElementById('book-list');
        let result = '';
        console.log(bookLists);
        if(bookLists.length>0){    
            bookLists.map((book,index)=>{
                result += `<tr>
                <td>${book.title}</td>
                <td>${book.author}</td>
                <td>${book.isbn}</td>
                <td><button class="btn btn-danger py-0" onclick="UI.deleteBook(${book.id})">Delete</button></td>
                </tr>`;
            })
        }
        else{
            result = `<h4>Books are not found</h4>`;
        }
        list.innerHTML = result;
        
    }

    showAlert(message,className){
        const div = document.createElement('div');

        // add classname
        div.className = `alert ${className}`;
        div.appendChild(document.createTextNode(message))

        const container = document.querySelector(".container");

        // get form 
        const form = document.querySelector("#book-form")

        // insert alert
        container.insertBefore(div,form);

        // timeout after 3 sec
        setTimeout(() => {
            document.querySelector(".alert").remove();
        }, 3000);
    }


    static deleteBook(target){
        // console.log(target);
        bookLists = bookLists.filter((book)=>book.id!=target);
        localStorage.setItem("list",JSON.stringify(bookLists)); 
        
        const ui = new UI();
        ui.showAlert('Book Removed','success');
        UI.displayBookList();
    }

    clearFields(){
        document.getElementById("title").value = "";
        document.getElementById("author").value = "";
        document.getElementById("isbn").value = "";
    }
}

let bookLists = [];
if (localStorage.getItem("list")) {
    // If it exists, parse the stored JSON and assign it to bookLists
    bookLists = JSON.parse(localStorage.getItem("list"));
    UI.displayBookList();
}
else{
    bookLists = [];
}

// event listening
document.getElementById("book-form").addEventListener("submit",(e)=>{
    e.preventDefault();
    
    const title = document.getElementById("title").value;
    const author = document.getElementById("author").value;
    const isbn = document.getElementById("isbn").value;
    const id = Math.ceil(Math.random()*1000000000);


    // instance of Book
    const book = new Book(id,title,author,isbn);
    console.log(book);
    // instance of UI
    const ui = new UI();

    // validate
    if(title==='' || author==='' || isbn===''){
        //  error alert 
        ui.showAlert('Please fill in all fields','error');
    }
    else{
        // add book to list
        ui.addBookToList(book);

        // show success
        ui.showAlert('Book Added','success')

        // clear fields
        ui.clearFields();
    }
})