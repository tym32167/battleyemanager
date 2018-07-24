import React, { Component } from 'react';
import { connect } from "react-redux";
import { serverActions } from "../../store/actions";
import {Error} from '../../controls';
import { EditForm } from './controls';

class Create extends Component {    
    render() {
        const { onSubmit, error } = this.props;

        return (
            <React.Fragment>
                <h2>Create Server</h2>
                <Error error={error} />      
                <EditForm onSubmit={onSubmit} />                            
            </React.Fragment>
        );
    }
}

const mapStateToProps = ({servers}) => {
    return {
        error:servers.error
    }
}

const mapDispatchToProps = (dispatch) => {
    return {        
        onSubmit: (item) => dispatch(serverActions.add(item))
    }
}

const ConnectedCreate = connect(
    mapStateToProps,
    mapDispatchToProps
)(Create);

export { ConnectedCreate as Create };