import { getJson } from "../Utils/GetJson.js"
import { formToObject } from "../Utils/GetFormData.js"
import { localDomain} from "../Utils/GetDomains.js"

export let formLogin = document.getElementById("formLogin");
export let formRegister = document.getElementById("formRegister");
export let btnRegister = document.querySelector(".btn-text-register");

export async function login(formLogin) {
    let formData = formToObject(formLogin);
    console.log(formData);
    let response = await getJson(localDomain + "api/AuthApi/Login", {
        method: "POST",
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(formData)
    });

    if (response) return true;
    else return false;
}

export function SwitchForm(formLogin,formRegister) {
        formRegister.classList.toggle("ocult");
        formLogin.classList.toggle("ocult");
}