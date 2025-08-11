import { getJson } from "../Utils/GetJson.js"
import { formToObject } from "../Utils/GetFormData.js"
import { localDomain} from "../Utils/GetDomains.js"

export let formRegister = document.getElementById("formRegister");
export let btnLogin = document.querySelector(".btn-text-login");

export async function register(foformRegisterrm) {
    let formData = formToObject(formRegister);
    let response = await getJson(localDomain + "api/AuthApi/Register", {
        method: "POST",
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(formData)
    });

    if (response) return true;
    else return false;



}