include ../../../Common/Build/config.mk

### target ###

TARGET_NAME   = ANTemplateToNTemplateCPP

CFLAGS += -w

### Objects, sources ###

# CPP source files

CXXSRCS = \
	ANTemplateToNTemplateCPP.cpp

ifeq ($(N_MAC_OSX_FRAMEWORKS),yes)
  LDLIBS ?= -framework NBiometricClient -framework NBiometrics -framework NMedia -framework NCore -framework NLicensing
else
  LDLIBS ?= -lNBiometricClient -lNBiometrics -lNMedia -lNCore -lNLicensing
endif

include ../../../Common/Build/rules.mk

