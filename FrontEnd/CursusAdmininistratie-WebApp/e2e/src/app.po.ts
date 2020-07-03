import { browser, by, element } from 'protractor';

export class AppPage {
  navigateTo(): Promise<unknown> {
    return browser.get(browser.baseUrl) as Promise<unknown>;
  }

  getTitleText(): Promise<string> {
    return element(by.css('app-cursus-list mat-card-title #com_title')).getText() as Promise<string>;
  }

  // getIets() {
  //   let wrapper = element.all(by.tagName('mat-card'))
  //     .filter(el => el.element(by.tagName('mat-card-title'))
  //     .getText()
  //     .then(text => {
  //       console.log('looking if\'', text, '\'contains Classifications');
  //       return text;
  //     }));


}
