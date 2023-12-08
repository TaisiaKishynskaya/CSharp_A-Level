import { makeAutoObservable } from "mobx";
import AuthStore from "../../AuthStores/AuthStore";

class RegistrationStore {
    private authStore: AuthStore;
    email = '';
    password = '';
    error = '';
    isLoading = false;

    constructor(authStore: AuthStore) {
        this.authStore = authStore;
        makeAutoObservable(this);
    }

    changeEmail(email: string) {
        this.email = email;
        if (!!this.error) {
            this.error = '';
        }
    }

    changePassword(password: string) {
        this.password = password;
        if (!!this.error) {
            this.error = '';
        }
    }

    async register() {
        try {
            this.isLoading = true;
            await this.authStore.register(this.email, this.password);
        }
        catch (e) {
            this.error = 'Registration Error';
        }
        finally {
            this.isLoading = false;
        }
    }
}

export default RegistrationStore;