import { connect } from 'react-redux';
import { Action, Dispatch } from 'redux';
import { IdTextList } from 'src/controls';
import { IBanReason } from 'src/models';
import { banReasonActions } from "../../store/actions";

const mapStateToProps = ({ banReasons }: { banReasons: any }) => {
    return {
        createUrl: "/banReasons/create",
        editUrl: "/banReasons/",
        error: banReasons.error,
        items: banReasons.items || [],
        title: "Ban reasons:",
    }
}

const mapDispatchToProps = (dispatch: Dispatch<Action<string>>) => {
    return {
        deleteItem: (item: IBanReason) => banReasonActions.deleteItem(item)(dispatch),
        onLoad: () => banReasonActions.getItems()(dispatch),
    }
}

const ConnectedList = connect(
    mapStateToProps,
    mapDispatchToProps
)(IdTextList);

export { ConnectedList as List };
