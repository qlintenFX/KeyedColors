# Changelog

All notable changes to KeyedColors will be documented in this file.

## [Unreleased]

### Added
- Added Dynamic Controls feature for real-time adjustments with hotkeys:
  - Shift+Up/Down arrows for gamma adjustment
  - Shift+Left/Right arrows for contrast adjustment
  - Toggle to enable/disable dynamic controls
  - Option to save current dynamic settings as a new profile

## [1.2.0] - 6-15-2025

### Added
- Added a dedicated Settings tab for application preferences
- Added "Start with Windows" option to automatically launch the app at system startup
- Added "Minimize to tray when closed" option to control app behavior when closing
- Settings are saved to Registry and persist between application restarts

## [1.1.0] - 4-18-2025

### Added
- Added PowerShell build script (`build-exe.ps1`) for developers to easily create self-contained Windows executable
- Added project logo and Ko-fi support button to README.md
- Added donation section with Ko-fi integration in README.md
- Added PayPal donation option to README.md

### Bug Fixes
- Fixed UI issue where the text input field in the "Add Profile" dialog was partially cut off, making the first characters difficult to read 