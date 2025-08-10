import { getJson } from "../Utils/GetJson.js"
import { localDomain } from "../Utils/GetDomains.js"

export async function LoadTags() {
    let data = await getJson(localDomain + "api/TaskApi/LoadTags", {});

    let allOptions = "";

    data.forEach(x => {
        let option = document.createElement("option");
        option.value = x.id;
        option.text = x.name;
        allOptions += option.outerHTML;
    });
    return allOptions;
}