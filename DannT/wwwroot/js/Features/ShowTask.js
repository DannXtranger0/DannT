import { getJson } from "../Utils/GetJson.js"
import { fillInput} from "../Utils/FillInput.js"
import { localDomain } from "../Utils/GetDomains.js"
import { LoadTags } from "../Utils/GetTags.js"
import { formToObject } from "../Utils/GetFormData.js"

export let form = document.querySelector("form");  
export let selectTagId = document.getElementById("TagId");

export let taskId = (window.location.pathname).split("/").at(-1);

export async function loadTask() {

    return await getJson(localDomain + `api/TaskApi/Task/${taskId}`, {});
}

export async function setUpTask(task) {
    fillInput("Title", task.title);
    fillInput("Description", task.description);
    fillInput("Deadline", task.deadline.split("T")[0]);

    //¿Por que antes me cargó los tags si el select lo tenía con un nombre distinto a como lo tengo en la función
    //Del otro archivo?
    selectTagId.innerHTML += await LoadTags();
    selectTagId.value = task.tagId;
}

export async function UpdateTask(form){
    let formData = formToObject(form);
    formData.TagId = selectTagId.options[selectTagId.selectedIndex].value;

    let response = await getJson(localDomain + `api/TaskApi/Update/${taskId}`, {
        method: "PUT",
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(formData)
    });

    return response ? true : false;
}

