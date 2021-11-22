import { Navigation } from './models/navigation.model';

let navigationPaths: Navigation[] =
[
    { Name: 'Home', Href: '/home', Icon: 'home' },
    { Name: 'Countries', Href: '/countries', Icon: 'flag' },
    { Name: 'Cities', Href: '/cities', Icon: 'apartment' },
];

export const NavigationPaths: Navigation[] = navigationPaths;
