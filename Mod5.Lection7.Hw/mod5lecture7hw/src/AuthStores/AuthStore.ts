import { makeAutoObservable } from "mobx";
import * as authApi from "../api/auth";

class AuthStore {
    token = "";
    id = "";

    constructor() {
        makeAutoObservable(this);
    }

    async login(email: string, password: string) {
        const result = await authApi.login({email, password});
        this.token = result.token;
    }

    async register(email: string, password: string) {
        const result = await authApi.register({email, password});
        this.id = result.id;
        this.token = result.token;
    }

    logout() {
        this.token = "";
    }
}

export default AuthStore;