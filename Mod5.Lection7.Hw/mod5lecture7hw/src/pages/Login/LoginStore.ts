import { makeAutoObservable } from "mobx";
import AuthStore from "../../AuthStores/AuthStore";

class LoginStore {
  private authStore: AuthStore;
  email = "";
  password = "";
  error = "";
  isLoading = false;

  constructor(authStore: AuthStore) {
    this.authStore = authStore;
    makeAutoObservable(this);
  }

  changeEmail(email: string) {
    this.email = email;
    if (!!this.error) {
      this.error = "";
    }
  }

  changePassword(password: string) {
    this.password = password;
    if (!!this.error) {
      this.error = "";
    }
  }

  async login() {
    try {
      this.isLoading = true;
      await this.authStore.login(this.email, this.password);
    } catch (e) {
      this.error = "Login Error";
    } finally {
      this.isLoading = false;
    }
  }
}

export default LoginStore;