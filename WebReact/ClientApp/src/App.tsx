import * as React from 'react';
import { Route, Routes } from 'react-router-dom';
import AppRoutes from './AppRoutes';
import { Layout } from './components/Layout';
import './custom.css';
import contactService from './services/ContactService';

export default class App extends React.Component {
    static displayName = App.name;

    constructor(props) {
        super(props);
    }

    async componentDidMount() {
        var data = await contactService.getContacts('sample');
        console.log(data.contacts);
    }

    render() {
        return (
            <Layout>
                <Routes>
                    {AppRoutes.map((route, index) => {
                        const { element, ...rest } = route;
                        return <Route key={index} {...rest} element={element} />;
                    })}
                </Routes>
            </Layout>
        );
    }
}
