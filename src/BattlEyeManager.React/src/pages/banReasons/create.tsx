import { connect } from "react-redux";
import { Action, Dispatch } from 'redux';
import { IdTextEdit } from "src/controls";
import { IBanReason } from 'src/models';
import { banReasonActions } from "../../store/actions";

const mapStateToProps = ({ banReasons }: { banReasons: any }) => {
    return {
        error: banReasons.error,
        form: "ban_reasaon_create",
        listUrl: '/banReasons',
        title: "Create ban reason",
    }
}

const mapDispatchToProps = (dispatch: Dispatch<Action>) => {
    return {
        onSubmit: (item: IBanReason) => banReasonActions.addItem(item)(dispatch)
    }
}

const ConnectedCreate = connect(
    mapStateToProps,
    mapDispatchToProps
)(IdTextEdit);

export { ConnectedCreate as Create };