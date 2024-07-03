import {
  transactions,
  getAllTransactionsDetails,
  downloadStatement
} from "./ajax.js";

const transaction = document.getElementById("btnTransactionSave");

transaction.addEventListener("click", async () => {
  const amount = document.getElementById("amount").value;
  const type = document.getElementById("type").value;

  await transactions(amount, type);
  getAllTransactions();
  closeModel();
});

const closeModel = () => {
  $("#btnClose").click();

  let alert = `<div class="alert alert-success d-flex align-items-center" role="alert">
                <div>
                    transactions has been successfully done 
                </div>
            </div>`;

  $("#showAlert").html(alert);
  setTimeout(() => {
    $("#showAlert").html("");
  }, 2000);
};

const getAllTransactions = async () => {
  const role = getCookies().role;
  const allTransactions = await getAllTransactionsDetails();
  allTransactions.reverse();
  let transactionList = ``;
  let ctr = 1;
  allTransactions.map(transaction => {
    transactionList += `<tr>
            <th scope="row">${ctr++}</th>
            <td>${transaction.E01F02}</td>
            <td>${transaction.E01F04}</td>
            <td>${transaction.A01F04}</td>
            <td>${transaction.A01F03}</td>
        </tr>`;
  });
  $("#tableTransactions").html(transactionList);
};

if(getCookies().role === "User"){

    const statement = document.getElementById("statement");
    
    statement.addEventListener("click", async () => {
        try {
        const res = await downloadStatement();
        
        const blob = new Blob([res], { type: "application/octet-stream" });
        const url = window.URL.createObjectURL(blob);
        const link = document.createElement("a");
        link.href = url;
        link.download = "statement.txt"; 
        link.click();
        
        console.log(link);
        } catch (error) {
        console.error("Error downloading statement:", error);
        alert("Error downloading statement: Please try again later.");
        }
    });

}
getAllTransactions();
