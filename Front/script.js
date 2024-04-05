let baseUrl = "http://localhost:8080/"

async function getUsersAsync(){

    let response = await fetch(baseUrl + "api/user/all");

    if(response.ok){

        let jsonData = await response.json();

        console.log(jsonData)
    }
}

async function postUserAsync(){

    const user = {
        fullname:"Anna",
        password:"anna",
        email:"anna@gmail.com"
    }

    let response = await fetch(baseUrl + "api/user/create", {
        method:"POST",
        headers: {
            'Content-Type': 'application/json;charset=utf-8'
        },
        body: JSON.stringify(user)
    });

    if(response.ok){
        console.log("SUCCESS!!!")
    }
}

let buttonGET = document.getElementById('btn-get');

buttonGET.addEventListener('click', async ()=>{
    await getUsersAsync();
});

let buttonPOST = document.getElementById('btn-post');

buttonPOST.addEventListener('click', async ()=>{
    await postUserAsync();
});