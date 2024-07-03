// import { getCookies } from "./access.js";

const BASE_URL = "https://localhost:44338";

export const signUp = async data => {
  return $.ajax({
    url: `${BASE_URL}/api/users/signup`,
    method: "POST",
    data: data,
    headers: {
      "Content-Type": "application/json"
    },
    success: function(result) {
      window.location.href = "./login.html";
      return result;
    },
    error: function(err) {
      return err;
    }
  });
};

export const login = async data => {
  return $.ajax({
    url: `${BASE_URL}/api/users/login`,
    method: "POST",
    data: data,
    headers: {
      "Content-Type": "application/json"
    },
    success: function(result) {
      return result;
    },
    error: function(err) {
      alert(err.responseJSON.Message);
      // return err.responseJSON.Message;
    }
  });
};

export const getAllusersData = async () => {
  let role = getCookies().role, token = getCookies().token;

  return $.ajax({
    url: `${BASE_URL}/api/users`,
    method: "GET",
    headers: {
      "Content-Type": "application/json",
      Authorization: `Bearer ${token}`
    },

    success: function(result) {
      return result;
    },
    error: function(err) {
      alert(err.responseJSON.Message);
    }
  });
};


export const getCurrentUser = async ()=>{
  let role = getCookies().role, token = getCookies().token;
  return $.ajax({
    url: `${BASE_URL}/api/users/me`,
    method: "GET",
    headers: {
      "Content-Type": "application/json",
      Authorization: `Bearer ${token}`
    },

    success: function(result) {
      return result;
    },
    error: function(err) {
      alert(err.responseJSON.Message);
    }
  });
}


export const UpdateUserName = async (name)=>{
  const data = JSON.stringify({
    E01F02 : name
  })

  let token = getCookies().token;

  return $.ajax({
    url : `${BASE_URL}/api/users`,
    method : "PUT",
    data : data,
    headers : {
      "Content-Type" : "application/json",
      Authorization : `Bearer ${token}`
    },
    success : function(result){
      return result;
    },
    error : function(err){
      alert(err.responseJSON.Message);
    }
  })
}


export const deleteUser = async()=>{
  let token = getCookies().token;

  return $.ajax({
    url : `${BASE_URL}/api/users`,
    method : "DELETE",
    headers : {
      "Content-Type" : "application/json",
      Authorization : `Bearer ${token}`
    },
    success : function(result){
      return result;
    },
    error : function(err){
      alert(err.responseJSON.Message);
    }
  })
}

export const transactions = (amount,type)=>{
  let token = getCookies().token;

  const data = JSON.stringify({
    A01F03: amount
  });

  let endPoint = type === 'D'? "deposit":"withdraw"; 
  return $.ajax({
    url : `${BASE_URL}/api/transactions/${endPoint}`,
    method : "POST",
    data:data,
    headers : {
      "Content-Type" : "application/json",
      Authorization : `Bearer ${token}`
    },
    success : function(result){
      return result;
    },
    error : function(err){
      alert(err.responseJSON.Message);
    }
  })
}

export const getAllTransactionsDetails = ()=>{
  let token = getCookies().token;
  let role = getCookies().role;
  const endPoint = role==="User" ? "/me" : '';
  return $.ajax({
    url: `${BASE_URL}/api/transactions/${endPoint}`,
    method: "GET",
    headers: {
      "Content-Type": "application/json",
      Authorization: `Bearer ${token}`
    },

    success: function(result) {
      return result;
    },
    error: function(err) {
      alert(err.responseJSON.Message);
    }
  });
}

export const downloadStatement = ()=>{
  let token = getCookies().token;
  return $.ajax({
    url: `${BASE_URL}/api/statements/me`,
    method: "GET",
    headers: {
      "Content-Type": "application/octet-stream",
      Authorization: `Bearer ${token}`
    },

    success: function(result) {
      return result;
    },
    error: function(err) {
      alert(err.responseJSON.Message);
    }
  });
} 