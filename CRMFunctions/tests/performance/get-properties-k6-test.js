import http from "k6/http";
import { sleep } from "k6";

export const options = {
    vus: 20,              
    duration: "10s",      
};

export default function () {
    http.get("http://localhost:7046/api/GetProperties");
    sleep(0.1);
}
