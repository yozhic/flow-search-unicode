# Search Unicode

Search Unicode is a set of Flow Launcher plugins to lookup and reverse lookup Unicode characters and emoji with their names. This is a port of [Search Unicode for Alfred] workflows.

[Search Unicode for Alfred]: https://github.com/blueset/alfred-search-unicode/

## Install

Once the plugins are published to the repository, you can install them directly from Flow Launcher using the `pm install` command.

```sh
pm install search unicode
```

Otherwise, you can download the latest release from the [releases page](https://github.com/blueset/flow-search-unicode/releases) and install it manually.

Unzip the content of the downloaded file to the Flow Launcher plugin directory at `%appdata%\FlowLauncher\Settings\Plugins\`, then restart your Flow Launcher.

## Usage

### Search character by description

![Screenshot for command u superscript](images/u_superscript.png)

Type `u keyword` (ex. `u superscript`) to get a list of characters
matching the keyword.

Press <kbd>Enter</kbd> to copy the character to clipboard (ex. `⁰`), press <kbd>→</kbd> to see and copy more information about the character.

![Screenshot for details of character �](images/u_details.png)

### Search character by code point

![Screenshot for command u fffd ff10](images/u_fffd_ff10.png)

Type `u codepoint [[codepoint] ...]` (ex. `u fffd ff10`) to look up characters by its codepoint.

### Search emoji by name

![Screenshot for command e face](images/e_face.png)

Type `e keywords` (ex. `e face`) to look up characters by its codepoint. Press <kbd>Enter</kbd> to copy the character to clipboard (ex. `😀`)

Additionally, you can choose your preferred emoji gender and skin tone in the workflow config. These settings are provided by `uni`. You can also search groups with `g:groupname` (ex. `e g:face-glasses`).

![Screenshot of workflow config for gender and skintones](images/e_config.png)

Press <kbd>Enter</kbd> to copy the character to clipboard (ex. `⁰`), press <kbd>→</kbd> to see and copy more information about the character.

![Screenshot for details of emoji 😀](images/e_details.png)

### Identify characters in a string

![Screenshot for command uid lenny face](images/uid_lenny.png)

Type `uid string` (ex. `uid ( ͡° ͜ʖ ͡°)`) to identify characters in a string.

Press <kbd>Enter</kbd> to copy the character to clipboard (ex. `⁰`), press <kbd>→</kbd> to see and copy more information about the character.

## Credit

This workflow depends on resources from:

- [arp242/uni] 2.8.0 with Unicode 15.1
- [Fluent Emoji] for emoji preview

[arp242/uni]: https://github.com/arp242/uni
[Fluent Emoji]: https://github.com/microsoft/fluentui-emoji

## License

```plain
Copyright 2024 Eana Hufwe <https://1a23.com>

Permission is hereby granted, free of charge, to any person
obtaining a copy of this software and associated documentation
files (the “Software”), to deal in the Software without
restriction, including without limitation the rights to use,
copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the
Software is furnished to do so, subject to the following
conditions:

The above copyright notice and this permission notice shall be
included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
OTHER DEALINGS IN THE SOFTWARE.
```
