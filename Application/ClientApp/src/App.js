import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { Status } from './components/Status';
import { Country } from './components/Country';
import './custom.css'
import { Detail } from './components/Detail';

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
        <Route exact path='/' component={Home} />
        <Route path='/status' component={Status} />
        <Route path='/country' component={Country} />
        <Route path='/details/:id/:redirectTo' component={Detail} />
      </Layout>
    );
  }
}
