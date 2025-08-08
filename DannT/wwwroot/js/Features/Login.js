import { getJson } from "../Utils/GetJson.js"
import { formToObject } from "../Utils/GetFormData.js"
import { localDomain} from "../Utils/GetDomains.js"

export let form = document.getElementById("formLogin");

export async function login(form) {
    let formData = formToObject(form);
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