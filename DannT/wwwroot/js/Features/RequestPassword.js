import { getJson } from "../Utils/GetJson.js"
import { formToObject } from "../Utils/GetFormData.js"
import { localDomain} from "../Utils/GetDomains.js"

export let form = document.querySelector("form");

export async function savePassword(form) {
    let formData = formToObject(form);
    let response = await getJson(localDomain + "api/AuthApi/SavePassword", {
        method: "POST",
        headers: {
            'Content-Type': 'application/json'
        },
        body:JSON.stringify(formData)
    });

    if (response) return true;
    else return false;

}