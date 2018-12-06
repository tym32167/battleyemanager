import React, { Component } from 'react';
import { connect } from 'react-redux';
import { Dispatch } from 'redux';
import { IAuthUserState } from 'src/store/models/IAuthUserState';
import { Error } from '../../controls';
import { authActions } from '../../store/actions';
import './login.css'

interface ILoginProps {
    login: (username: string, password: string) => void,
    error?: any
}

interface ILoginState {
    password?: string,
    submitted?: boolean,
    username?: string,
}

class Login extends Component<ILoginProps, ILoginState> {

    constructor(props: ILoginProps) {
        super(props);

        this.state = {
            password: '',
            submitted: false,
            username: '',
        };

        this.handleChange = this.handleChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }

    public handleChange(e: React.FormEvent<EventTarget>) {
        const target = e.target as HTMLInputElement;
        const { name, value } = target;
        this.setState({ [name]: value });
    }

    public handleSubmit(e: any) {
        e.preventDefault();

        this.setState({ submitted: true });
        const { username, password } = this.state;
        const { login } = this.props;
        if (username && password) {
            login(username, password);
        }
    }

    public render() {
        const { username, password } = this.state;
        const { error } = this.props;
        return (
            <div className="text-center body">
                <form className="form-signin" onSubmit={this.handleSubmit}>
                    <h1 className="h3 mb-3 font-weight-normal">Please sign in</h1>
                    <div className="form-group">
                        <input type="text" className="form-control" name="username" placeholder="User Name"
                            value={username} onChange={this.handleChange} required={true} />
                    </div>
                    <div className="form-group">
                        <input type="password" className="form-control" name="password" placeholder="Password"
                            value={password} onChange={this.handleChange} required={true} />
                    </div>
                    <Error error={error} />
                    <button className="btn btn-lg btn-primary btn-block">Sign in</button>
                    <hr />
                    <footer>
                        <p>&copy; 2018 - BattlEye Manager</p>
                    </footer>
                </form>
            </div>);
    }
}

const mapStateToProps = ({ auth }: { auth: IAuthUserState }) => {
    return {
        error: auth.error
    }
}

function mapDispatchToProps(dispatch: Dispatch<void>) {
    return {
        login: (username: any, password: any) => dispatch(authActions.login(username, password))
    }
}

const connectedLogin = connect(mapStateToProps, mapDispatchToProps)(Login);
export { connectedLogin as Login };
