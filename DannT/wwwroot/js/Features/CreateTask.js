import { getJson } from "../Utils/GetJson.js"
import { formToObject } from "../Utils/GetFormData.js"
import { localDomain} from "../Utils/GetDomains.js"

export let form = document.querySelector("form");  
let selectTagId = document.getElementById("TagId");

export async function Create(form) {
    let formData = formToObject(form);
    formData.TagId = selectTagId.options[selectTagId.selectedIndex].value;

    let response = await getJson(localDomain + "api/TaskApi/Create", {
        method: "POST",
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(formData)
    });

    return (response) ? true : false;
}

export async function LoadTags() {
    let data = await getJson(localDomain + "api/TaskApi/LoadTags", {});

    data.forEach(x => {
        let option = document.createElement("option");
        option.value = x.id;
        option.text = x.name;

        selectTagId.appendChild(option);
    })

}